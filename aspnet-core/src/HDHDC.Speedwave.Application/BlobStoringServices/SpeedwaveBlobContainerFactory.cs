using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public class SpeedwaveBlobContainerFactory : BlobContainerFactory, ISpeedwaveBlobContainerFactory
    {
        public SpeedwaveBlobContainerFactory(
           IBlobContainerConfigurationProvider configurationProvider,
           ICurrentTenant currentTenant,
           ICancellationTokenProvider cancellationTokenProvider,
           IBlobProviderSelector providerSelector,
           SpeedwaveFileSystemBlobProvider speedwaveFileSystemBlobProvider,
           IServiceProvider serviceProvider)
            : base(configurationProvider, currentTenant, cancellationTokenProvider, providerSelector, serviceProvider)
        {
            Provider = speedwaveFileSystemBlobProvider;
        }

        public SpeedwaveFileSystemBlobProvider Provider { get; }

        public override IBlobContainer Create(string name)
        {
            var configuration = ConfigurationProvider.Get(name);
            var blobProvider = ProviderSelector.Get(name);

            if (blobProvider.GetType().IsAssignableTo<ISpeedwaveBlobProvider>())
            {
                return new SpeedwaveBlobContainer(
                    name,
                    configuration,
                    (ISpeedwaveBlobProvider)blobProvider,
                    CurrentTenant,
                    CancellationTokenProvider,
                    ServiceProvider);
            }
            else
            {
                return new BlobContainer(
                    name,
                    configuration,
                    blobProvider,
                    CurrentTenant,
                    CancellationTokenProvider,
                    ServiceProvider);
            }
        }
    }
}
