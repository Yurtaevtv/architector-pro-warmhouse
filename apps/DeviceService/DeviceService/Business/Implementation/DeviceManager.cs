using DeviceService.Components.MessageBroker;
using DeviceService.DAL.Repostory;
using DeviceService.Models;

namespace DeviceService.Business.Implementation
{
    internal sealed class DeviceManager(
                            IDeviceRepository deviceRepository,
                            IDeviceEventPublisher eventPublisher) : IDeviceManager
    {
        public Task<Device?> GetByIdAsync(int id)
        {
            return deviceRepository.GetByIdAsync(id);
        }

        public Task<Device[]> GetAllAsync()
        {
            return deviceRepository.GetAllAsync();
        }

        public Task AddDevice(DeviceMetadata deviceInfo)
        {
            return deviceRepository.AddDevice(deviceInfo)
                .ContinueWith(async r =>
                {
                    if (r.IsCompletedSuccessfully)
                    {
                        await eventPublisher.PublishAsync(new
                        {
                            eventType = "add new device",
                            context = new
                            {
                                name = deviceInfo.Name,
                                type = deviceInfo.Type.ToString("G")
                            }
                        });
                    }
                });
        }
    }
}
