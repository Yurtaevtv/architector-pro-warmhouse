using Microsoft.AspNetCore.Mvc;

namespace Temperature.Api.Controller
{
    [ApiController]
    [Route("{controller}")]
    public class TemperatureController : ControllerBase
    {
        [HttpGet("{sensorId}")]
        public IActionResult GetById(string sensorId)
        {
            Random rnd = new Random();
            return Ok(new {Value =rnd.Next(0, 40), Status = "active"});
        }

        [HttpGet]
        public IActionResult GetByLocation([FromQuery]string location)
        {
            Random rnd = new Random();
            return Ok(new {Value =rnd.Next(0, 40), Status = "active"});
        }

    }
}
