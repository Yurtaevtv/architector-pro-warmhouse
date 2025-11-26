using DeviceService.Api.Components.Clients;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DeviceService.Api.Components.Health
{
    internal class SmartHomeClientHealthCheck(ISmartHomeClient smartHomeClient) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await smartHomeClient.HealthAsync() ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}
