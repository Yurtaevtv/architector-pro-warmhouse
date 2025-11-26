using System.ComponentModel.DataAnnotations;

namespace Telemetry.Api.Models.Settings
{
    public class KafkaSettings
    {
        [Required]
        public string? BootstrapServers { get; init; }

        [Required]
        public string? DeviceTelemetryTopic { get; init; } = "device-telemetry";
    }
}
