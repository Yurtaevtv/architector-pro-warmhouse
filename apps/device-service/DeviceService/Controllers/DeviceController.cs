using DeviceService.Business;
using DeviceService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.Controllers
{
    [ApiController]
    [Route("/app/devices")]
    public class DeviceController(
            IDeviceManager deviceManager) : Controller
    {


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await deviceManager.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await deviceManager.GetByIdAsync(id));
        }

        /// <summary>
        /// УБИТЬ ТУТ
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Add(DeviceCreateRequest device)
        {
            try
            {
                await deviceManager.AddDevice(device);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem($"Can`t add device. {e.Message}");
            }
        }

    }
}
