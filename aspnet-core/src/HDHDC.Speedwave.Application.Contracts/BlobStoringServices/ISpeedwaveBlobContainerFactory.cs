using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public interface ISpeedwaveBlobContainerFactory : IBlobContainerFactory
    {
    }
}
