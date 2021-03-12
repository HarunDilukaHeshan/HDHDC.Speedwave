using HDHDC.Speedwave.BlobServices;
using HDHDC.Speedwave.BlobServices.Containers;
using HDHDC.Speedwave.BlobStoringServices;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing.Imaging;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Data;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace HDHDC.Speedwave
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(SpeedwaveDomainModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule)
        )]
    public class SpeedwaveTestBaseModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpIdentityServerBuilderOptions>(options =>
            {
                options.AddDeveloperSigningCredential = false;
            });

            PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            {
                identityServerBuilder.AddDeveloperSigningCredential(false, System.Guid.NewGuid().ToString());
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSpeedwaveBlobContainer<CategoryThumbnailsBlobContainer>();
            context.Services.AddSpeedwaveBlobContainer<ItemPicturesBlobContainer>();

            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });

            context.Services.AddAlwaysAllowAuthorization();

            Configure<AbpBlobStoringOptions>(options =>
            {
                var blobStorage = @".\blogstorage";

                options.Containers.AddSpeedwaveBlobContainer<CategoryThumbnailsBlobContainer>(config => {
                    config.BasePath = blobStorage;
                });

                options.Containers.AddSpeedwaveBlobContainer<ItemPicturesBlobContainer>(config => {
                    config.BasePath = blobStorage;
                });

                options.Containers.AddSpeedwaveBlobContainer<StoreChainLogoBlobContainer>(config => {
                    config.BasePath = blobStorage;
                });
            });

            Configure<SpeedwaveBlobOptions>(options => {
                options.Prefix = "Speedwave_";
                options.CategoryThumbnail = new SpeedwaveBlobSubOptions
                {
                    Prefix = "Category_",
                    ImageFormat = ImageFormat.Jpeg,
                    Width = 300,
                    Height = 300
                };
                options.UserAvatar = new SpeedwaveBlobSubOptions
                {
                    Prefix = "Avatar_",
                    ImageFormat = ImageFormat.Jpeg,
                    Width = 600,
                    Height = 600
                };
                options.ItemThumbnail = new SpeedwaveBlobSubOptions
                {
                    Prefix = "Item_",
                    ImageFormat = ImageFormat.Jpeg,
                    Width = 300,
                    Height = 300
                };
                options.SlideShow = new SpeedwaveBlobSubOptions
                {
                    Prefix = "SlideShow_",
                    ImageFormat = ImageFormat.Jpeg,
                    Width = 1366,
                    Height = 768
                };
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
