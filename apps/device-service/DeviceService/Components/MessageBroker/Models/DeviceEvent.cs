using System.Text.Json.Serialization;

namespace DeviceService.Components.MessageBroker.Models
{
    public class DeviceEvent
    {
        [JsonPropertyName("device_id")]
        public int DeviceId { get; init; }

        [JsonPropertyName("event")]
        public string Event { get; init; }
    }
}
