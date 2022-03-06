using Microsoft.AspNetCore.Mvc;
using SharedMediaStreamer.Domain.Interfaces;

namespace ShareMediaStreamer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepository;
        public MediaController(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return File(_mediaRepository.GetMediaContents(0), "application/octet-stream");
        }
    }
}
