using DeviceService.Api.Models.Rest;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.Api.Business
{
    public interface IDeviceManager
    {
        Task<SensorResponse[]> GetAllAsync(CancellationToken httpContextRequestAborted);
        Task<SensorResponse?> GetByIdAsync(int deviceId, CancellationToken httpContextRequestAborted);
        Task TurnOffAsync(int deviceId);
        Task TurnOnAsync(int deviceId);
    }
}
