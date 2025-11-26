using Telemetry.Api.Components.MessageBroker;
using Telemetry.Api.Components.MessageBroker.Messages;

namespace Telemetry.Api.Business.Implementation
{
    public class TelemetryManager(IDeviceMessagePublisher messagePublisher) : ITelemetryManager
    {
        public Task PushTelemetryMesage(DeviceMetric metric)
        {
            return messagePublisher.PublishDeviceMetric(metric);
        }
    }
}
