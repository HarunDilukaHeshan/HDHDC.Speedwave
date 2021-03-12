using HDHDC.Speedwave.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace HDHDC.Speedwave.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SpeedwaveEntityFrameworkCoreDbMigrationsModule),
        typeof(SpeedwaveApplicationContractsModule)
        )]
    public class SpeedwaveDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
