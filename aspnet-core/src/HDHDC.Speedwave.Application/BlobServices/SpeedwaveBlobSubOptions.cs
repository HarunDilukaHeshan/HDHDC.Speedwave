using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HDHDC.Speedwave.BlobServices
{
    public class SpeedwaveBlobSubOptions
    {
        public string Prefix { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
