using DeviceService.Api.Components.Clients.Rest;

namespace DeviceService.Api.Components.Clients;

internal interface ISmartHomeClient
{

    Task<bool> HealthAsync();

    Task<Sensor[]> TryGetAllSensors();

    Task<Sensor?> TryGetSensor(int sensorId);

    Task<Sensor?> TryCreateSensor(SensorRequest sensorInfo);

    Task<bool> TryUpdateSensor(int sensorId, SensorRequest sensorInfo);

    Task<bool> TryDeleteSensor(int sensorId);
}