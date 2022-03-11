using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedMediaStreamer.Domain.Models
{
    public class VideoDetails : MediaDetails
    {
        public VideoDetails()
        {
            this.ContentType = "video/mp4";
        }
    }
}
