namespace SharedMediaStreamer.MediaDataProcessor.Interfaces
{
    public interface IMediaFileReader
    {
        void GetMediaByteContents(byte[] buffer, int offset, int length);
    }
}
