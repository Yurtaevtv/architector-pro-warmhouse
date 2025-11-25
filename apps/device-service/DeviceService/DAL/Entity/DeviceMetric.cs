namespace DeviceService.DAL.Entity
{
    public class DeviceMetric
    {

        public int DeviceId { get; init; }

        public Device Device { get; init; }

        public int MetricId { get; init; }

        public Metric Metric { get; init; }

        public string Value { get; init; }
    }
}
