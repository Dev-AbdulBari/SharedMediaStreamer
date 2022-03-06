using SharedMediaStreamer.Domain.Interfaces;

namespace SharedMediaStreamer.Domain
{
    public class VideoRepository : IMediaRepository
    {
        public byte[] GetMediaContents(uint offset, short bufferSizeInMB = 30)
        {
            throw new NotImplementedException();
        }
    }
}
