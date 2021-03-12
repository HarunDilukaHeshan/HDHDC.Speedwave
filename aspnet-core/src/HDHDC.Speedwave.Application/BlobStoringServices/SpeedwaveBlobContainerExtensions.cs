using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public static class SpeedwaveBlobContainerExtensions
    {
        public static BlobContainerConfigurations AddSpeedwaveBlobContainer<TContainer>(
            this BlobContainerConfigurations containerConfig,
            Action<FileSystemBlobProviderConfiguration> config)
            where TContainer : class
        {            

            containerConfig.Configure<TContainer>(c =>
            {
                c.UseFileSystem(config);
                c.ProviderType = typeof(ISpeedwaveBlobProvider);
            });

            return containerConfig;
        }

        public static BlobContainerConfigurations AddSpeedwaveDefaultBlobContainer(
            this BlobContainerConfigurations containerConfig, 
            Action<FileSystemBlobProviderConfiguration> config)
        {
            containerConfig.ConfigureDefault(container =>
            {
                container.UseFileSystem(config);
                container.ProviderType = typeof(ISpeedwaveBlobProvider);
            });
            return containerConfig;
        }

        public static IServiceCollection AddSpeedwaveBlobContainer<TContainer>(this IServiceCollection services)
            where TContainer : class
        {
            return services.AddTransient<ISpeedwaveBlobContainer<TContainer>, SpeedwaveBlobContainer<TContainer>>();
        }

        public static IServiceCollection AddSpeedwaveDefaultBlobContainer(this IServiceCollection services)
        {
            services.AddTransient<ISpeedwaveBlobContainer<DefaultContainer>, SpeedwaveBlobContainer<DefaultContainer>>();

            services.AddTransient(
                typeof(ISpeedwaveBlobContainer),
                serviceProvider => serviceProvider
                    .GetRequiredService<ISpeedwaveBlobContainer<DefaultContainer>>());

            return services;
        }

        public static async Task<IList<byte[]>> GetBytesListAsync(
            this ISpeedwaveBlobContainer blobContainer, 
            string searchPattern, 
            int skipCount, 
            int maxResultCount = 10)
        {
            var bytesList = await blobContainer.GetListAsync(searchPattern, skipCount, maxResultCount);

            return bytesList.ToList();
        }
    }
}
