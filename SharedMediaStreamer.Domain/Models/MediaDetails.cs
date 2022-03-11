using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMediaStreamer.Domain.Models
{
    public abstract class MediaDetails
    {
        public string FileName { get; set; }
        public long FileLength { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public long BufferSizeInBytes { get; set; }
    }
}
