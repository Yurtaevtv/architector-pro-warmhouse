using Confluent.Kafka;
using DeviceService.Api.Components.MessageBroker.Messages;
using DeviceService.Api.Models.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DeviceService.Api.Components.MessageBroker.Implementation
{
    public class DeviceMessagePublisher : IDeviceMessagePublisher
    {
        private readonly ILogger<DeviceMessagePublisher> _logger;
        private readonly IOptions<KafkaSettings> _kafkaSettings;
        private readonly IProducer<Null, string> _stringProducer;


        public DeviceMessagePublisher(
            IOptions<KafkaSettings> kafkaSettings,
            ILogger<DeviceMessagePublisher> logger)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.Value.BootstrapServers,
            };

            _stringProducer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task PublishEventDevice(DeviceEvent @event)
        {
            string message = JsonSerializer.Serialize(@event);

            _logger.LogTrace("Publish event: {Event}", message);

            await _stringProducer.ProduceAsync(_kafkaSettings.Value.DeviceCommandsTopic, new Message<Null, string>
            {
                Value = message
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _stringProducer.Dispose();
        }


    }
}
