using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace HDHDC.Speedwave.BlobServices.Options
{
    public class StoreChainLogoBlobOptions
    {
        public string Prefix { get; set; } = "storechain_";
        public ImageFormat ImageFormat { get; set; } = ImageFormat.Jpeg;
        public int Width { get; set; } = 128;
        public int Height { get; set; } = 128;
    }
}
