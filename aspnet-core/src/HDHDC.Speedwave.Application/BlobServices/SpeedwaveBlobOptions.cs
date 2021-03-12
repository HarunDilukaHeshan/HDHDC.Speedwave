using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HDHDC.Speedwave.BlobServices
{
    public class SpeedwaveBlobOptions
    {
        public string Prefix { get; set; }
        public SpeedwaveBlobSubOptions CategoryThumbnail { get; set; }
        public SpeedwaveBlobSubOptions UserAvatar { get; set; }
        public SpeedwaveBlobSubOptions ItemThumbnail { get; set; }
        public SpeedwaveBlobSubOptions SlideShow { get; set; }
    }
}
