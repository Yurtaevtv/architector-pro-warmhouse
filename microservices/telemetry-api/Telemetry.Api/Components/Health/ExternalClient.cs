using Microsoft.Extensions.Diagnostics.HealthChecks;
using Telemetry.Api.Components.Clients;

namespace Telemetry.Api.Components.Health
{
    internal class SmartHomeClientHealthCheck(ISmartHomeClient smartHomeClient) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await smartHomeClient.HealthAsync() ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}
