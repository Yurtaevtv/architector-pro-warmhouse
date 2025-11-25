namespace DeviceService.DAL.Entity
{
    public class Device
    {

        public int Id { get; init; }

        public string? Name { get; init; }

        public string? DeviceUrl { get; init; }

        public DeviceType Type { get; init; }
        public int TypeId { get; init; }

        public ICollection<Metric> Metrics { get; init; } = [];
        public ICollection<DeviceMetric> DeviceMetrics { get; init; } = [];



        public ICollection<Action> Actions { get; init; } = [];
    }
}
