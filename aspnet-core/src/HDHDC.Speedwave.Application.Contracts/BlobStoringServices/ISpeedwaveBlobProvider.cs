using HDHDC.Speedwave.BlobServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public interface ISpeedwaveBlobProvider : IBlobProvider
    {
        Task<IEnumerable<byte[]>> GetListAsync(BlobProviderGetListArgs args);
        Task<IEnumerable<BlobFile>> GetListWithFileInfoAsync(BlobProviderGetListArgs args);
    }
}
