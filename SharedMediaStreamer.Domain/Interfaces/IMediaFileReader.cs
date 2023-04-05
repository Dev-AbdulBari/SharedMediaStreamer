using SharedMediaStreamer.Domain.Models;

namespace SharedMediaStreamer.Domain.Interfaces
{
    public interface IMediaFileReader
    {
        MediaDetails GetVideo(long offset, int length);
    }
}
