using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace HDHDC.Speedwave.EntityFrameworkCore
{
    [DependsOn(
        typeof(SpeedwaveEntityFrameworkCoreModule)
        )]
    public class SpeedwaveEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SpeedwaveMigrationsDbContext>();
        }
    }
}
