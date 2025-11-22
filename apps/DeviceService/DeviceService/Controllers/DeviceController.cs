using DeviceService.DAL.Repostory;
using DeviceService.Models;
using DeviceService.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DeviceService.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class DeviceController(
            IDeviceRepository deviceRepository) : Controller
    {


        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Json(new DeviceCollection { Devices = await deviceRepository.GetAllAsync() });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await deviceRepository.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Add(DeviceMetadata device)
        {
            try
            {
                await deviceRepository.AddDevice(device);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem($"Can`t add device. {e.Message}");
            }
        }

    }
}
