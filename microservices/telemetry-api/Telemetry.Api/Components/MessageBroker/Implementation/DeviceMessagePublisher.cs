using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Telemetry.Api.Components.MessageBroker.Messages;
using Telemetry.Api.Models.Settings;

namespace Telemetry.Api.Components.MessageBroker.Implementation
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

        public async Task PublishDeviceMetric(DeviceMetric metric)
        {
            string message = JsonSerializer.Serialize(metric);

            _logger.LogTrace("Publish event: {Event}", message);

            await _stringProducer.ProduceAsync(_kafkaSettings.Value.DeviceTelemetryTopic, new Message<Null, string>
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
