using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices
{
    public interface ISpeedwaveBlobContainer : ITransientDependency
    {
        Task<byte[]> GetBytesAsync(string name);

        Task<string> SaveBytesAsync(string name, byte[] bytes);

        Task DeleteAsync(string name);
    }
}
