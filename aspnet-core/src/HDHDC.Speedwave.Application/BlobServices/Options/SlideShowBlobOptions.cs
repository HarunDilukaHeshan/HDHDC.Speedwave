using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HDHDC.Speedwave.BlobServices.Options
{
    public class SlideShowBlobOptions
    {
        public string Prefix { get; set; } = "slideshow_";
        public string BasePath { get; set; } = "";
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Jpeg;
        public int Width { get; set; } = 1336;
        public int Height { get; set; } = 768;
    }
}
