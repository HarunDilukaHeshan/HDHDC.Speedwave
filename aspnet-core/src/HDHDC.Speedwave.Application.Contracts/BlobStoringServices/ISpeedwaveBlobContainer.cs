using HDHDC.Speedwave.BlobServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public interface ISpeedwaveBlobContainer<TContainer> : ISpeedwaveBlobContainer
        where TContainer : class
    { }

    public interface ISpeedwaveBlobContainer : IBlobContainer, ITransientDependency
    {
        Task<IEnumerable<byte[]>> GetListAsync(
            string searchPattern,
            int skipCount,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<BlobFile>> GetListWithFileInfoAsync(
            string searchPattern,
            int skipCount,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default);
    }
}
