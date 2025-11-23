using System.ComponentModel.DataAnnotations;

namespace WebSocketService.Models
{
    public class KafkaSettings
    {
        [Required]
        public string? BootstrapServers { get; init; }

        [Required]
        public string? DeviceCommandsTopic { get; init; } = "device-command";

        [Required]
        public string? DeviceEventsTopic { get; init; } = "device-event";

        [Required]
        public string? DeviceTelemetryTopic { get; init; } = "device-telemetry";
    }
}
