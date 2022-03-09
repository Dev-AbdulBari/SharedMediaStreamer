using Microsoft.AspNetCore.Mvc;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.Domain.Models;

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
        public IActionResult GetMedia()
        {
            if (Request.Headers.Range.ToString() == "") return BadRequest("Requested content range is invalid");

            var contentOffset = ExtractNumericValuesFromRequestRange(Request.Headers.Range);
            var mediaDetails = _mediaRepository.GetMedia(contentOffset);

            SetResponseHeaders(contentOffset, mediaDetails);

            return File(mediaDetails.Content, mediaDetails.ContentType);
        }

        private void SetResponseHeaders(int contentOffset, MediaDetails mediaDetails)
        {
            var contentBufferSize = Math.Min(contentOffset + mediaDetails.BufferSizeInBytes, mediaDetails.FileLength - 1);

            Response.Headers.AcceptRanges = "bytes";
            Response.Headers.ContentRange = $"bytes {contentOffset}-{contentBufferSize}/{mediaDetails.FileLength}";
            Response.Headers.ContentLength = mediaDetails.FileLength;
            Response.StatusCode = 206;
        }

        private int ExtractNumericValuesFromRequestRange(string range)
        {
            string extractedNumericValuesAsString = "";

            foreach(char c in range)
            {
                if (char.IsDigit(c)) extractedNumericValuesAsString += c;
            }

            return int.Parse(extractedNumericValuesAsString);
        }
    }
}
