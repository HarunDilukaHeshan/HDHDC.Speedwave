using HDHDC.Speedwave.BlobServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace HDHDC.Speedwave.BlobStoringServices
{
    public class SpeedwaveBlobContainer<TContainer> : ISpeedwaveBlobContainer<TContainer>
        where TContainer : class
    {
        private readonly ISpeedwaveBlobContainer _container;

        public SpeedwaveBlobContainer(ISpeedwaveBlobContainerFactory blobContainerFactory)
        { 
            _container = (ISpeedwaveBlobContainer)blobContainerFactory.Create<TContainer>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <param name="overrideExisting"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            return _container.SaveAsync(
                name,
                stream,
                overrideExisting,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.DeleteAsync(
                name,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.ExistsAsync(
                name,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> GetAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.GetAsync(
                name,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> GetOrNullAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return _container.GetOrNullAsync(
                name,
                cancellationToken
            );
        }        

        public Task<IEnumerable<byte[]>> GetListAsync(
            string searchPattern, 
            int skipCount, 
            int maxResultCount, 
            CancellationToken cancellationToken = default)
        {
            return _container.GetListAsync(searchPattern, skipCount, maxResultCount);
        }
        public Task<IEnumerable<BlobFile>> GetListWithFileInfoAsync(
           string searchPattern,
           int skipCount,
           int maxResultCount,
           CancellationToken cancellationToken = default)
        {
            return _container.GetListWithFileInfoAsync(searchPattern, skipCount, maxResultCount);
        }
    }

    public class SpeedwaveBlobContainer : BlobContainer, ISpeedwaveBlobContainer
    {
        protected new ISpeedwaveBlobProvider Provider { get; }

        public SpeedwaveBlobContainer(
            string containerName,
            BlobContainerConfiguration configuration,
            ISpeedwaveBlobProvider provider,
            ICurrentTenant currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IServiceProvider serviceProvider)
            : base(containerName, configuration, provider, currentTenant, cancellationTokenProvider, serviceProvider)
        {
            Provider = provider;
        }        

        public async Task<IEnumerable<byte[]>> GetListAsync(
            string searchPattern, 
            int skipCount, 
            int maxResultCount = 10, 
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Change(GetTenantIdOrNull()))
            {
                var (normalizedContainerName, normalizedBlobName) =
                    NormalizeNaming(ContainerName, "");

                return await Provider.GetListAsync(new BlobProviderGetListArgs(
                    ContainerName, 
                    Configuration, 
                    searchPattern, 
                    skipCount, 
                    maxResultCount));
            }
        }

        public async Task<IEnumerable<BlobFile>> GetListWithFileInfoAsync(
            string searchPattern,
            int skipCount,
            int maxResultCount = 10,
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Change(GetTenantIdOrNull()))
            {
                var (normalizedContainerName, normalizedBlobName) =
                    NormalizeNaming(ContainerName, "");

                return await Provider.GetListWithFileInfoAsync(new BlobProviderGetListArgs(
                    ContainerName,
                    Configuration,
                    searchPattern,
                    skipCount,
                    maxResultCount));
            }
        }
    }
}
