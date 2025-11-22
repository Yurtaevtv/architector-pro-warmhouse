using DeviceService.Models.Enums;

namespace DeviceService.Models
{
    public record DeviceMetadata
    {

        public string? Name { get; init; }

        public DeviceType Type { get; init; }

        public Metric[] AvailableMetrics { get; init; } = [];

        public DeviceAction[] Actions { get; init; } = [];
    }
}
