using SharedMediaStreamer.Domain.Models;

namespace SharedMediaStreamer.Domain.Interfaces
{
    public interface IMediaFileReader
    {
        MediaDetails GetVideo(int offset, int length);
    }
}
