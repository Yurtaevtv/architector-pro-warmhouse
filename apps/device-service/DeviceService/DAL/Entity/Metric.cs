namespace DeviceService.DAL.Entity
{
    public class Metric
    {
        public int Id { get; init; }
        public string Name { get; init; }

        public ICollection<Device> Devices { get; init; }
        public ICollection<DeviceMetric> DeviceMetrics { get; init; }
    }
}
