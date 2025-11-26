using DeviceService.Api.Business;
using DeviceService.Api.Models.Rest;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/device")]
    public class DeviceController(IDeviceManager deviceManager) : Controller
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetSensors()
        {
            SensorResponse[] sensors = await deviceManager.GetAllAsync(HttpContext.RequestAborted);

            return sensors.Any() ? Json(sensors) : NoContent();
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetSensor(int deviceId)
        {
            SensorResponse? sensor = await deviceManager.GetByIdAsync(deviceId, HttpContext.RequestAborted);
            return sensor is not null ? Json(sensor) : NoContent();
        }

    }
}
