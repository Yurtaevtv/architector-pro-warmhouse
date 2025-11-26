using DeviceService.Api.Business;
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
            return Json(await deviceManager.GetAllAsync(HttpContext.RequestAborted));
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetSensor(int deviceId)
        {
            return Json(await deviceManager.GetByIdAsync(deviceId, HttpContext.RequestAborted));
        }

    }
}
