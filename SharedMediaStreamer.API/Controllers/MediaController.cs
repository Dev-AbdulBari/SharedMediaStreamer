using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.Domain.Models;
using SharedMediaStreamer.Domain.Models.Settings;

namespace ShareMediaStreamer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaRepository _mediaRepository;
        private string _folderPath;
        public MediaController(IMediaRepository mediaRepository, IOptions<MediaSettings> mediaSettings)
        {
            _mediaRepository = mediaRepository;
            _folderPath = mediaSettings.Value.FolderPath;
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

        [HttpGet]
        [Route("subtitles")]
        public IActionResult GetSubtitles()
        {
            var subtitleFile = $"{_folderPath}\\subtitles.vtt";

            if (System.IO.File.Exists(subtitleFile))
            {
                return File(System.IO.File.ReadAllBytes(subtitleFile), "text/vtt");
            }

            return NotFound();
        }

        private void SetResponseHeaders(long contentOffset, MediaDetails mediaDetails)
        {
            var contentBufferSize = Math.Min(contentOffset + mediaDetails.BufferSizeInBytes, mediaDetails.FileLength - 1);

            Response.Headers.AcceptRanges = "bytes";
            Response.Headers.ContentRange = $"bytes {contentOffset}-{contentBufferSize}/{mediaDetails.FileLength}";
            Response.Headers.ContentLength = mediaDetails.FileLength;
            Response.StatusCode = 206;
        }

        private long ExtractNumericValuesFromRequestRange(string range)
        {
            string extractedNumericValuesAsString = "";

            foreach(char c in range)
            {
                if (char.IsDigit(c)) extractedNumericValuesAsString += c;
            }

            return long.Parse(extractedNumericValuesAsString);
        }
    }
}
