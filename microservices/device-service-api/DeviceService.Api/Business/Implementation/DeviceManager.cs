using DeviceService.Api.Components.Clients;
using DeviceService.Api.Components.Clients.Rest;
using DeviceService.Api.Components.MessageBroker;
using DeviceService.Api.Components.MessageBroker.Messages;
using DeviceService.Api.Models.Rest;

namespace DeviceService.Api.Business.Implementation
{
    internal class DeviceManager(
                        ISmartHomeClient client,
                        IDeviceMessagePublisher messagePublisher) : IDeviceManager
    {
        public async Task<SensorResponse[]> GetAllAsync(CancellationToken httpContextRequestAborted)
        {
            Sensor[] sensors = await client.TryGetAllSensors();

            return sensors.Select(s => new SensorResponse()
            {
                CreateAt = s.CreateAt,
                Id = s.Id,
                IsActive = s.Status == "active",
                LastUpdate = s.LastUpdate,
                Location = s.Location,
                Name = s.Name,
                Type = s.Type,
                Unit = s.Unit,
                Value = s.Value
            }).ToArray();

        }

        public async Task<SensorResponse?> GetByIdAsync(int deviceId, CancellationToken httpContextRequestAborted)
        {
            Sensor? s = await client.TryGetSensor(deviceId);
            if (s is null)
            {
                return null;
            }

            return new SensorResponse()
            {
                CreateAt = s.CreateAt,
                Id = s.Id,
                IsActive = s.Status == "active",
                LastUpdate = s.LastUpdate,
                Location = s.Location,
                Name = s.Name,
                Type = s.Type,
                Unit = s.Unit,
                Value = s.Value
            };
        }

        public async Task TurnOffAsync(int deviceId)
        {
            bool res = await client.TryUpdateSensor(deviceId, new SensorRequest()
            {
                Status = "inactive"
            });

            if (!res)
            {
                throw new InvalidOperationException("Can`t turn off device");
            }

            await messagePublisher.PublishEventDevice(new DeviceEvent()
            {
                DeviceId = deviceId,
                Event = "turn-off"
            });
        }

        public async Task TurnOnAsync(int deviceId)
        {
            bool res = await client.TryUpdateSensor(deviceId, new SensorRequest()
            {
                Status = "active"
            });

            if (!res)
            {
                throw new InvalidOperationException("Can`t turn on device");
            }

            await messagePublisher.PublishEventDevice(new DeviceEvent()
            {
                DeviceId = deviceId,
                Event = "turn-on"
            });
        }
    }
}
