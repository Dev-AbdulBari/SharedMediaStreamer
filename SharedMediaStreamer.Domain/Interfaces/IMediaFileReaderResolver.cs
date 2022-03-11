using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMediaStreamer.Domain.Interfaces
{
    public interface IMediaFileReaderResolver
    {
        IMediaFileReader GetMediaFileReader<T>();
    }
}
