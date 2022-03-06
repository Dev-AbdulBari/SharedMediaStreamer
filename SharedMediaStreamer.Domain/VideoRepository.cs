using SharedMediaStreamer.Domain.Interfaces;

namespace SharedMediaStreamer.Domain
{
    public class VideoRepository : IMediaRepository
    {
        private IMediaFileReader _mediaFileReader;
        public VideoRepository(IMediaFileReader mediaFileReader)
        {
            _mediaFileReader = mediaFileReader;
        }

        public byte[] GetMediaContents(int offset, short bufferSizeInMB = 5)
        {
            var bufferSizeInBytes = bufferSizeInMB * 1_000_000;
            var buffer = new byte[bufferSizeInBytes];

            _mediaFileReader.GetMediaByteContents(buffer, offset, bufferSizeInBytes);

            return buffer;
        }
    }
}
