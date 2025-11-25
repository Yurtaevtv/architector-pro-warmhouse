using DeviceService.DAL.Entity;
using DeviceService.Models;

namespace DeviceService.DAL.Repostory
{
    public interface IDeviceRepository
    {
        /// <summary>
        /// get device by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Device?> GetByIdAsync(int id);

        /// <summary>
        /// get all devices
        /// </summary>
        /// <returns></returns>
        Task<Device[]> GetAllAsync();

        /// <summary>
        /// Append new device
        /// </summary>
        /// <param name="deviceInfo"></param>
        /// <returns></returns>
        Task<Device> AddDevice(DeviceCreateRequest deviceInfo);
    }
}
