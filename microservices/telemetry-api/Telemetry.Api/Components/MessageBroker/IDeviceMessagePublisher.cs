using Telemetry.Api.Components.MessageBroker.Messages;

namespace Telemetry.Api.Components.MessageBroker
{
    public interface IDeviceMessagePublisher : IDisposable
    {

        Task PublishDeviceMetric(DeviceMetric metric);

    }
}
