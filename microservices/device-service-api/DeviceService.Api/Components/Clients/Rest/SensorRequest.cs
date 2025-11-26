using System.Text.Json.Serialization;

namespace DeviceService.Api.Components.Clients.Rest
{
    internal class SensorRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("location")]
        public string? Location { get; init; }

        [JsonPropertyName("unit")]
        public string? Unit { get; init; }

        [JsonPropertyName("status")]
        public string? Status { get; init; }
    }
}
