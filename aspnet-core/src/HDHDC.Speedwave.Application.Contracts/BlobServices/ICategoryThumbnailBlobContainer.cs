using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices
{
    public interface ICategoryThumbnailBlobContainer : ISpeedwaveBlobContainer, ITransientDependency
    {
        Task<string> SaveBytesAsync(byte[] bytes);
    }
}
