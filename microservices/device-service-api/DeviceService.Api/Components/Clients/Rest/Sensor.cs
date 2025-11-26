using System.Text.Json.Serialization;

namespace DeviceService.Api.Components.Clients.Rest
{
    public class Sensor
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }
        [JsonPropertyName("name")]
        public string? Name { get; init; }
        [JsonPropertyName("type")]
        public string? Type { get; init; }
        [JsonPropertyName("location")]
        public string? Location { get; init; }
        [JsonPropertyName("value")]
        public double Value { get; init; }
        [JsonPropertyName("unit")]
        public string? Unit { get; init; }
        [JsonPropertyName("status")]
        public string? Status { get; init; }
        [JsonPropertyName("last_updated")]
        public DateTime LastUpdate { get; init; }
        [JsonPropertyName("created_at")]
        public DateTime CreateAt { get; init; }

    }
}
