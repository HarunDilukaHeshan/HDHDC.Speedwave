using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HDHDC.Speedwave.EntityFrameworkCore;
using HDHDC.Speedwave.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using HDHDC.Speedwave.BlobServices;
using System.Drawing.Imaging;
using HDHDC.Speedwave.BlobStoringServices;
using HDHDC.Speedwave.BlobServices.Containers;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Microsoft.AspNetCore.Identity;
using HDHDC.Speedwave.tokenProviders;

namespace HDHDC.Speedwave
{
    [DependsOn(
        typeof(SpeedwaveHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(SpeedwaveApplicationModule),
        typeof(SpeedwaveEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpBlobStoringFileSystemModule)
        )]
    public class SpeedwaveHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";        

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureUrls(configuration);
            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureVirtualFileSystem(context);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureBlobStorage(context, configuration);                        
        }

        private void ConfigureBlobStorage(ServiceConfigurationContext context, IConfiguration configuration)
        {           
            context.Services.AddSpeedwaveBlobContainer<CategoryThumbnailsBlobContainer>();
            context.Services.AddSpeedwaveBlobContainer<ItemPicturesBlobContainer>();
            context.Services.AddSpeedwaveBlobContainer<StoreChainLogoBlobContainer>();
            context.Services.AddSpeedwaveBlobContainer<SlideShowBlobContainer>();

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

                options.Containers.AddSpeedwaveBlobContainer<SlideShowBlobContainer>(config => {
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

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<SpeedwaveDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HDHDC.Speedwave.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<SpeedwaveDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HDHDC.Speedwave.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<SpeedwaveApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HDHDC.Speedwave.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<SpeedwaveApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}HDHDC.Speedwave.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(SpeedwaveApplicationModule).Assembly, opts => {
                    opts.TypePredicate = type => false;
                });
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "Speedwave";
                    options.JwtBackChannelHandler = new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };                   
                });

            context.Services.AddAbpIdentity()
                .AddPasswordResetTotpTokenProvider();
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "Speedwave API", Version = "v1"});
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });            
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();    
            
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAbpRequestLocalization();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Speedwave API");                
            });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
