using System.Text.Json.Serialization;

namespace DeviceService.Components.MessageBroker.Models
{
    public class DeviceCommandEvent
    {
        [JsonPropertyName("device_id")]
        public int DeviceId { get; init; }

        [JsonPropertyName("action")]
        public string Action { get; init; }
    }
}
