using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace HDHDC.Speedwave.UtilityServices
{
    public class ImageManipulator : IImageManipulator
    {
        public bool IsImageStream(string base64Img)
        {
            var b64 = base64Img.Contains(',') ? base64Img.Substring(base64Img.IndexOf(',') + 1) : base64Img;
            byte[] buffer;

            try
            {
                buffer = Convert.FromBase64String(b64);
            } 
            catch(Exception)
            {
                return false;
            }

            return IsImageStream(buffer);
        }
        public bool IsImageStream(byte[] byteStream)
        {
            bool isImageStream = true;
            MemoryStream st = null;
            Image img = null;

            try
            {
                st = new MemoryStream(byteStream);
                img = Image.FromStream(st);
            }
            catch (Exception)
            {
                isImageStream = false;
            }
            finally
            {
                st?.Dispose();
                img?.Dispose();
            }

            return isImageStream;
        }
        public byte[] ResizeImage(byte[] byteStream, int width, int height, ImageFormat imageFormat)
        {
            Image destiImg = new Bitmap(width, height);
            byte[] destiStream = null;

            if (!IsImageStream(byteStream)) throw new ArgumentException("ByteStream does not represent an image");

            using var imgStream = new MemoryStream(byteStream);
            using var sourceImg = Image.FromStream(imgStream);
            using (var graphics = Graphics.FromImage(destiImg))
            {
                graphics.DrawImage(sourceImg, 0, 0, width, height);
                graphics.Save();

                using var st = new MemoryStream();
                destiImg.Save(st, imageFormat);
                destiStream = st.GetBuffer();
            }

            destiImg.Dispose();

            return destiStream;
        }
        public string ResizeImage(string base64Img, int width, int height, ImageFormat imageFormat)
        {
            if (!IsImageStream(base64Img)) throw new ArgumentException();

            var b64 = base64Img.Contains(',') ? base64Img.Substring(base64Img.IndexOf(',') + 1) : base64Img;

            var buffer = Convert.FromBase64String(b64);

            var resizedImgBuffer = ResizeImage(buffer, width, height, imageFormat);

            return Convert.ToBase64String(resizedImgBuffer);
        }
    }
}
