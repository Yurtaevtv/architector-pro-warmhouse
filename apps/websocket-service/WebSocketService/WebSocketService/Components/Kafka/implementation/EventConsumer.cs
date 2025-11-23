using Confluent.Kafka;
using Microsoft.Extensions.Options;
using WebSocketService.Models;

namespace WebSocketService.Components.Kafka.implementation
{
    public abstract class EventConsumer : IWSConsumer
    {
        private CancellationTokenSource _tokenSource;
        private Task? _consumingTask;
        private readonly IConsumer<string, string> _consumer;

        protected readonly IOptions<KafkaSettings> _settings;
        protected readonly ILogger<EventConsumer> _logger;
        protected abstract string Topic { get; }

        public EventConsumer(
            IOptions<KafkaSettings> settings,
            ILogger<EventConsumer> logger)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = settings.Value.BootstrapServers,
                GroupId = settings.Value.DeviceCommandsTopic,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnableAutoCommit = false
            };
            _settings = settings;
            _logger = logger;
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        protected abstract Task ProcessKafkaMessage(string message);

        public async Task StartConsumingAsync()
        {

            if (_consumingTask != null)
            {
                _logger.LogError($"Consuming task for {_settings.Value.DeviceCommandsTopic} can`t be restarted");
            }

            _tokenSource = new CancellationTokenSource();

            _consumer.Subscribe(_settings.Value.DeviceCommandsTopic);
            _logger.LogInformation("Kafka WebSocket consumer started. Topics: {Topics}", _settings.Value.DeviceCommandsTopic);

            while (!_tokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(_tokenSource.Token);

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

        public Task StopConsumersAsync()
        {
            return _tokenSource.CancelAsync();
        }

        public void Dispose()
        {
            _tokenSource.Cancel();
            _consumer.Dispose();
        }
    }
}
