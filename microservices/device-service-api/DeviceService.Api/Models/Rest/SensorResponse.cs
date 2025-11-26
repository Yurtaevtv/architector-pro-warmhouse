namespace DeviceService.Api.Models.Rest
{
    public class SensorResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Type { get; init; }
        public required string Location { get; init; }
        public required double Value { get; init; }
        public required string Unit { get; init; }
        public required bool IsActive { get; init; }
        public required DateTime LastUpdate { get; init; }
        public required DateTime CreateAt { get; init; }
    }
}
