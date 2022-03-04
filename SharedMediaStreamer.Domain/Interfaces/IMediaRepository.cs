namespace SharedMediaStreamer.Domain.Interfaces
{
    public interface IMediaRepository
    {
        byte[] GetMediaContents(uint offset, short bufferSizeInMB = 30);
    }
}
