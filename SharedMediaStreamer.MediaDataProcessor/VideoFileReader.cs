using Microsoft.Extensions.Options;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.Domain.Models;
using SharedMediaStreamer.Domain.Models.Settings;

namespace SharedMediaStreamer.MediaDataProcessor
{
    public class VideoFileReader : IMediaFileReader
    {
        private string _pathToTestVideoFile;

        public VideoFileReader(IOptions<MediaSettings> mediaSettings)
        {
            _pathToTestVideoFile = $"{mediaSettings.Value.FolderPath}\\{mediaSettings.Value.FileName}";
        }

        public MediaDetails GetVideo(long offset, int length)
        {
            using (FileStream fileStream = File.Open(_pathToTestVideoFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var buffer = new byte[length];
                fileStream.Seek(offset, SeekOrigin.Begin);
                fileStream.Read(buffer, 0, length);

                return new VideoDetails() {
                    FileName = fileStream.Name,
                    FileLength = fileStream.Length,
                    Content = buffer
                };
            }
        }
    }
}
