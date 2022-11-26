using Microsoft.Extensions.Options;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.Domain.Models;
using SharedMediaStreamer.Domain.Models.Settings;

namespace SharedMediaStreamer.Domain
{
    public class VideoRepository : IMediaRepository
    {
        private IMediaFileReader _mediaFileReader;
        private short _bufferSizeInMB;
        public VideoRepository(IMediaFileReaderResolver mediaFileReaderResolver, IOptionsMonitor<MediaSettings> mediaSettings)
        {
            _mediaFileReader = mediaFileReaderResolver.GetMediaFileReader<VideoRepository>();
            _bufferSizeInMB = mediaSettings.CurrentValue.BufferSizeInMB;
        }

        public MediaDetails GetMedia(long offset)
        {
            return _mediaFileReader.GetVideo(offset, GetBufferSizeInBytes());
        }

        private int GetBufferSizeInBytes()
        {
            return _bufferSizeInMB * 1_000_000;
        }
    }
}
