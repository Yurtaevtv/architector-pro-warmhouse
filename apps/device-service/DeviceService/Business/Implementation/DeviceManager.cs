using AutoMapper;
using DeviceService.Components.MessageBroker;
using DeviceService.DAL.Entity;
using DeviceService.DAL.Repostory;
using DeviceService.Models;

namespace DeviceService.Business.Implementation
{
    internal sealed class DeviceManager(
                            IDeviceRepository deviceRepository,
                            IDeviceEventPublisher eventPublisher,
                            IMapper mapper
                            ) : IDeviceManager
    {
        public async Task<DeviceInfo?> GetByIdAsync(int id)
        {
            Device? device = await deviceRepository.GetByIdAsync(id);
            if (device is null)
            {
                return null;
            }

            return mapper.Map<Device, DeviceInfo>(device);
        }

        public async Task<DeviceCollection> GetAllAsync()
        {
            Device[] devices = await deviceRepository.GetAllAsync();

            return new DeviceCollection()
            {
                Devices = devices.Select(d => mapper.Map<Device, DeviceInfo>(d)).ToArray()
            };
        }

        public async Task AddDevice(DeviceMetadata deviceInfo)
        {
            Device device = await deviceRepository.AddDevice(deviceInfo);
            await eventPublisher.PublishDeviceEventAsync(new()
            {
                DeviceId = device.Id,
                Event = "new"
            });
        }
    }
}
