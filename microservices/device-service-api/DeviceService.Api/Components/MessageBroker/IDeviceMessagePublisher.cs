using DeviceService.Api.Components.MessageBroker.Messages;

namespace DeviceService.Api.Components.MessageBroker
{
    public interface IDeviceMessagePublisher : IDisposable
    {

        Task PublishEventDevice(DeviceEvent @event);


    }
}
