using SharedMediaStreamer.Domain.Interfaces;

namespace SharedMediaStreamer.MediaDataProcessor
{
    public class VideoFileReader : IMediaFileReader
    {
        public void GetMediaByteContents(byte[] buffer, int offset, int length)
        {
            using (FileStream fileStream = File.Open(@"C:\Media\Test.mp4", FileMode.Open))
            {
                fileStream.Read(buffer, offset, length);
            }
        }
    }
}
