using Microsoft.AspNetCore.Mvc;

namespace jwt_auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetResult()
        {
            return Ok("You are Authorized now");
        }
       
    }
}
