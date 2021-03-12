using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.UtilityServices
{
    public interface IImageManipulator : ITransientDependency
    {
        bool IsImageStream(string base64Img);
        bool IsImageStream(byte[] byteStream);        
        byte[] ResizeImage(byte[] byteStream, int width, int height, ImageFormat imageFormat);
        string ResizeImage(string base64Img, int width, int height, ImageFormat imageFormat);
    }
}
