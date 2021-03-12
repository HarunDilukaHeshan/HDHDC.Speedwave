using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HDHDC.Speedwave.BlobServices.Options
{
    public class CategoryThumbnailsBlobOptions
    {
        public string Prefix { get; set; } = "category_";
        public string BasePath { get; set; } = "";
        public string ThumbnailPath { get; set; } = "thumbnail";
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Jpeg;
        public int Width { get; set; } = 128;
        public int Height { get; set; } = 128;
    }
}
