using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DeviceService.Components.MessageBroker.Models
{
    public class DeviceTelemetryEvent
    {

        [JsonPropertyName("device_id")]
        public int DeviceId { get; init; }

        [JsonPropertyName("metric")]
        public string Metric { get; init; }

        [JsonPropertyName("value")]
        public JsonValue Value { get; init; }
    }
}
