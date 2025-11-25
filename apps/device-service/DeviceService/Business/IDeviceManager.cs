using DeviceService.DAL.Entity;
using DeviceService.Models;

namespace DeviceService.Business
{
    public interface IDeviceManager
    {

        /// <summary>
        /// get device by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeviceInfo?> GetByIdAsync(int id);

        /// <summary>
        /// get all devices
        /// </summary>
        /// <returns></returns>
        Task<DeviceCollection> GetAllAsync();

        /// <summary>
        /// Add new device to the system. Publish message
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        Task AddDevice(DeviceCreateRequest deviceInfo);

    }
}