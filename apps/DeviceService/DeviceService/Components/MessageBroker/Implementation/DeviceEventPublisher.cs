using System.Text.Json;

namespace DeviceService.Components.MessageBroker.Implementation
{
    internal class DeviceEventPublisher(ILogger<DeviceEventPublisher> logger) : IDeviceEventPublisher
    {
        public Task PublishAsync(object @event)
        {
            logger.LogInformation("Publish event: {Event}", JsonSerializer.SerializeToDocument(@event).ToString());
            return Task.CompletedTask;
        }
    }
}
