using SharedMediaStreamer.Domain.Models;

namespace SharedMediaStreamer.Domain.Interfaces
{
    public interface IMediaRepository
    {
        MediaDetails GetMedia(long offset);
    }
}
