using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.Services
{
    public class TestDataProvider : ITransientDependency
    {
        public byte[] GetPicture()
        {
            var img = new Bitmap(800, 450);
            byte[] buffer = new byte[0];

            using (var graphics = Graphics.FromImage(img))
            {
                var pen = new Pen(Color.Red, 10f);
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                graphics.DrawLine(pen, 0, 0, 300, 300);
                graphics.DrawArc(pen, new Rectangle(0, 0, 120, 120), 37f, 82f);
                graphics.Save();

                using var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);

                using FileStream fs = System.IO.File.Create("test.jpeg");
                img.Save(fs, ImageFormat.Jpeg);

                buffer = fs.GetAllBytes();
            }

            img.Dispose();

            return buffer;
        }
    }
}
