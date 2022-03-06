using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMediaStreamer.Domain.Models
{
    public class MediaDetails
    {
        public long ContentLength { get; set; }
        public string ContentRange { get; set; }
        public string ContentType { get; set; }
    }
}
