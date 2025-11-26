using System.Text.Json;
using Telemetry.Api.Components.Clients.Rest;

namespace Telemetry.Api.Components.Clients
{
    internal class SmartHomeClient(
                        ILogger<SmartHomeClient> logger,
                        IHttpClientFactory clientFactory) : ISmartHomeClient
    {
        private HttpClient _client => clientFactory.CreateClient(nameof(SmartHomeClient));

        public async Task<bool> HealthAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("/health");
            return response.IsSuccessStatusCode;
        }

        public async Task<Sensor[]> TryGetAllSensors()
        {
            HttpResponseMessage response = await _client.GetAsync($"api/v1/sensors/");

            if (!response.IsSuccessStatusCode)
            {
                return [];
            }

            return JsonSerializer.Deserialize<Sensor[]>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Sensor?> TryGetSensor(int sensorId)
        {
            HttpResponseMessage response = await _client.GetAsync($"api/v1/sensors/{sensorId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Sensor>(await response.Content.ReadAsStringAsync());
        }

        public async Task<Sensor?> TryCreateSensor(SensorRequest sensorInfo)
        {
            using JsonContent content = JsonContent.Create(sensorInfo);
            HttpResponseMessage response = await _client.PostAsync("/api/v1/sensors/", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Sensor>(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> TryUpdateSensor(int sensorId, SensorRequest sensorInfo)
        {
            using JsonContent content = JsonContent.Create(sensorInfo);
            HttpResponseMessage response = await _client.PutAsync($"/api/v1/sensors/{sensorId}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> TryDeleteSensor(int sensorId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/v1/sensors/{sensorId}");
            return response.IsSuccessStatusCode;
        }
    }
}
