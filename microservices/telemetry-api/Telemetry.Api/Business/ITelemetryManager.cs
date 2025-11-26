using Telemetry.Api.Components.MessageBroker.Messages;

namespace Telemetry.Api.Business
{
    public interface ITelemetryManager
    {
        Task PushTelemetryMesage(DeviceMetric metric);

    }
}
