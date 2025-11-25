namespace DeviceService.Models
{
    public record DeviceCreateRequest
    {

        public string Name { get; init; }
        public string DeviceType { get; init; }
        public string[] AvailableCommands { get; init; }
        public string[] AvailableMetrics { get; init; }

    }
}
