using DeviceService.Models.Enums;

namespace DeviceService.Models
{
    public record Device : DeviceMetadata
    {

        public int Id { get; init; }

        internal Device(DeviceMetadata metadata)
        {
            Actions = metadata.Actions;
            AvailableMetrics = metadata.AvailableMetrics;
            Name = metadata.Name;
            Type = metadata.Type;
        }
    }
}
