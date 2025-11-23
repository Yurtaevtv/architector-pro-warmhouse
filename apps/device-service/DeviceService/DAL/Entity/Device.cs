namespace DeviceService.DAL.Entity
{
    public class Device
    {

        public int Id { get; init; }

        public string? Name { get; init; }

        public string DeviceUrl { get; init; }

        public int TypeId { get; init; }

        public DeviceType Type { get; init; }

        public ICollection<DeviceMetric> AvailableMetrics { get; init; } = [];

        public ICollection<DeviceAction> Actions { get; init; } = [];
    }
}
