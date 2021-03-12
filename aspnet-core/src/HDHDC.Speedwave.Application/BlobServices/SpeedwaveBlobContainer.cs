using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobServices
{
    public class SpeedwaveBlobContainer
    {
        protected IBlobContainer BlobContainer { get; }

        protected SpeedwaveBlobOptions Options { get; }

        protected string BlobPrefix { get; }

        public SpeedwaveBlobContainer(IBlobContainer blobContainer, IOptions<SpeedwaveBlobOptions> options)
        {
            BlobContainer = blobContainer;
            Options = options.Value;
            BlobPrefix = Options.Prefix;
        }

        public virtual async Task<byte[]> GetBytesAsync(string name)
        {            
            return await BlobContainer.GetAllBytesAsync(name);
        }

        public virtual async Task<string> SaveBytesAsync(string name, byte[] bytes)
        {
            var fileName = BlobPrefix + name;
            await BlobContainer.SaveAsync(fileName, bytes);
            return fileName;
        }        

        public virtual async Task DeleteAsync(string name)
        {
            await BlobContainer.DeleteAsync(name);
        }
    }
}
