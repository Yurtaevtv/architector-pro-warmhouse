using Confluent.Kafka;
using DeviceService.Components.MessageBroker.Models;
using DeviceService.Models.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DeviceService.Components.MessageBroker.Implementation
{
    internal class DeviceEventPublisher : IDeviceEventPublisher
    {
        private ILogger<DeviceEventPublisher> _logger;
        private IOptions<KafkaSettings> _kafkaSettings;
        private IProducer<Null, string> _stringProducer;


        public DeviceEventPublisher(
                IOptions<KafkaSettings> kafkaSettings,
                ILogger<DeviceEventPublisher> logger)
        {
            _logger = logger;
            _kafkaSettings = kafkaSettings;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.Value.BootstrapServers,
            };

            _stringProducer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task PublishDeviceEventAsync(DeviceEvent @event)
        {
            string message = JsonSerializer.Serialize(@event);

            _logger.LogInformation("Publish event: {Event}", message);

            await _stringProducer.ProduceAsync(_kafkaSettings.Value.DeviceCommandsTopic, new Message<Null, string>
            {
                Value = message
            });
        }

        public void Dispose()
        {
            _stringProducer.Dispose();
        }
    }
}
