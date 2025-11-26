using DeviceService.Api.Business;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/device-manager")]
    public class DeviceManagerController(IDeviceManager deviceManager) : Controller
    {

        [HttpPost("turn-on/{deviceId}")]
        public async Task<IActionResult> TurnOn(int deviceId)
        {
            await deviceManager.TurnOnAsync(deviceId);
            return Ok();
        }

        [HttpPost("turn-off/{deviceId}")]
        public async Task<IActionResult> TurnOff(int deviceId)
        {
            await deviceManager.TurnOffAsync(deviceId);
            return Ok();
        }

    }
}
