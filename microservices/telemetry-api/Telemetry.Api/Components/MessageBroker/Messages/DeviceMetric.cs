using System.Text.Json.Serialization;

namespace Telemetry.Api.Components.MessageBroker.Messages
{
    public class DeviceMetric
    {
        [JsonPropertyName("device_id")]
        public int DeviceId { get; init; }

        [JsonPropertyName("unit")]
        public required string Unit { get; init; }

        [JsonPropertyName("value")]
        public required double Value { get; init; }
    }
}

