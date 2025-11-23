using DeviceService.Components.MessageBroker.Models;

namespace DeviceService.Components.MessageBroker
{
    public interface IDeviceEventPublisher : IDisposable
    {
        /// <summary>
        /// publish event to essage broker
        /// </summary>
        Task PublishDeviceEventAsync(DeviceEvent @event);

    }
}
