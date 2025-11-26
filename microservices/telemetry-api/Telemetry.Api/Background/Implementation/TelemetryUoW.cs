using Microsoft.Extensions.Options;
using Telemetry.Api.Business;
using Telemetry.Api.Components.Clients;
using Telemetry.Api.Components.Clients.Rest;
using Telemetry.Api.Components.MessageBroker.Messages;
using Telemetry.Api.Components.Store;
using Telemetry.Api.Models.Settings;

namespace Telemetry.Api.Background.Implementation
{
    internal class TelemetryUoW(
                ITelemetryStore telemetryStore,
                ITelemetryManager telemetryManager,
                ISmartHomeClient smartHomeClient,
                IOptions<AppSettings> settings) : IUnitOfWork
    {
        private Semaphore _locker = new Semaphore(1, 1);

        public string Schedule => settings.Value.Schedule;

        public async Task InvokeAsync()
        {
            if (telemetryStore.All.Any() && _locker.WaitOne())
            {
                try
                {
                    Sensor[] sensors = (await Task.WhenAll(telemetryStore.All.Select(smartHomeClient.TryGetSensor)))
                                            .Where(s => s is not null)
                                            .Where(s => s.Status == "active")
                                            .Select(s => s!)
                                            .ToArray();

                    await Task.WhenAll(sensors
                        .Select(s => new DeviceMetric()
                        {
                            DeviceId = s.Id,
                            Unit = s.Unit,
                            Value = s.Value
                        })
                        .Select(telemetryManager.PushTelemetryMesage));
                }
                finally
                {
                    _locker.Release();
                }
            }
        }
    }
}
