using Microsoft.AspNetCore.Mvc;
using Telemetry.Api.Components.Store;

namespace Telemetry.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/telemetry")]
    public class TelemetryController(ITelemetryStore telemetryStore) : Controller
    {
        /// <summary>
        /// Turn on telemetry for device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost("turn-on/{deviceId}")]
        public async Task<IActionResult> TurnOnForDevice(int deviceId)
        {
            if (await telemetryStore.TryAdd(deviceId))
            {
                return Ok();
            }

            return Problem("Telemetry was not included");
        }

        /// <summary>
        /// Turn off telemetry for device
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost("turn-off/{deviceId}")]
        public IActionResult TurnOffForDevice(int deviceId)
        {
            telemetryStore.Remove(deviceId);
            return Ok();
        }

    }
}
