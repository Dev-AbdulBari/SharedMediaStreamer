namespace SharedMediaStreamer.Domain.Interfaces
{
    public interface IMediaRepository
    {
        byte[] GetMediaContents(int offset, short bufferSizeInMB = 5);
    }
}
