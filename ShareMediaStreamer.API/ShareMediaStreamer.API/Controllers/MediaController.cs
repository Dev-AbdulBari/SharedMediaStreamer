using Microsoft.AspNetCore.Mvc;

namespace ShareMediaStreamer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Add media controller actions here
            return Ok();
        }
    }
}
