using DeviceService.Models;

namespace DeviceService.DAL.Repostory.Implementation
{
    internal class DeviceRepository : IDeviceRepository
    {

        private Dictionary<int, Device> _devices = [];

        public Task<Device?> GetByIdAsync(int id)
        {
            return Task.FromResult(_devices.GetValueOrDefault(id));
        }

        public Task<Device[]> GetAllAsync()
        {
            return Task.FromResult(_devices.Values.ToArray());
        }

        public Task AddDevice(DeviceMetadata deviceInfo)
        {
            return Task.Run(() =>
            {
                _devices.Add(_devices.Count, new Device(deviceInfo) { Id = _devices.Count });
            });
        }
    }
}
