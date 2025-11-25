using Confluent.Kafka;
using Microsoft.Extensions.Options;
using WebSocketService.Models;

namespace WebSocketService.Components.Kafka.implementation
{
    public abstract class EventConsumer : IWSConsumer
    {
        private readonly IConsumer<string, string> _consumer;

        protected readonly IOptions<KafkaSettings> _settings;
        protected readonly ILogger<EventConsumer> _logger;
        protected abstract string Topic { get; }

        public EventConsumer(
            IOptions<KafkaSettings> settings,
            ILogger<EventConsumer> logger)
        {
            _settings = settings;

            var config = new ConsumerConfig
            {
                BootstrapServers = settings.Value.BootstrapServers,
                GroupId = Topic,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false
            };
            _logger = logger;
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        protected abstract Task ProcessKafkaMessage(string message);

        public async Task StartConsumingAsync(CancellationTokenSource cts)
        {

            _consumer.Subscribe(_settings.Value.DeviceCommandsTopic);
            _logger.LogInformation("Kafka WebSocket consumer started. Topics: {Topics}", _settings.Value.DeviceCommandsTopic);

            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cts.Token);

                    if (consumeResult?.Message?.Value != null)
                    {
                        await ProcessKafkaMessage(consumeResult.Message.Value);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Error consuming message from Kafka");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error in Kafka consumer");
                }
            }

        }


        public void Dispose()
        {
            _consumer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
