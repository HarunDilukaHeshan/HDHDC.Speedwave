using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HDHDC.Speedwave.BlobServices.Options
{
    public class ItemPicturesBlobOptions
    {
        public string Prefix { get; set; } = "item_";
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Jpeg;
        public int Width { get; set; } = 300;
        public int Height { get; set; } = 300;
        public int ThumbnailWidth { get; set; } = 128;
        public int ThumbnailHeight { get; set; } = 128;
    }
}
