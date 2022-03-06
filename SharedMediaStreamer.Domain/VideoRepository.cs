using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.MediaDataProcessor.Interfaces;

namespace SharedMediaStreamer.Domain
{
    public class VideoRepository : IMediaRepository
    {
        private IMediaFileReader _mediaFileReader;
        public VideoRepository(IMediaFileReader mediaFileReader)
        {
            _mediaFileReader = mediaFileReader;
        }
    }
}
