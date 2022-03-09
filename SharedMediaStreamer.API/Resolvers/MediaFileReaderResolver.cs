using SharedMediaStreamer.Domain;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.MediaDataProcessor;

namespace SharedMediaStreamer.API.Resolvers
{
    public class MediaFileReaderResolver : IMediaFileReaderResolver
    {
        private IServiceProvider _serviceProvider;
        public MediaFileReaderResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IMediaFileReader GetMediaFileReader<T>()
        {
            return typeof(IMediaFileReader) switch
            {
                var givenType when typeof(T) == typeof(VideoRepository) => GetMediaFileReaderForRepository<T>(typeof(VideoFileReader)),
                _ => throw new NotImplementedException()
            };
        }

        private IMediaFileReader GetMediaFileReaderForRepository<T>(Type mediaFileType)
        {
            return _serviceProvider.GetServices<IMediaFileReader>().Single(s => s.GetType() == mediaFileType);
        }
    }
}
