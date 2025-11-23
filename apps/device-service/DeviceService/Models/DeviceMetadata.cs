namespace DeviceService.Models
{
    public record DeviceMetadata
    {

        public string Name { get; init; }
        public string DeviceType { get; init; }
        public string[] AvailableCommands { get; init; }
        public string[] AvailableMetrics { get; init; }

    }
}
