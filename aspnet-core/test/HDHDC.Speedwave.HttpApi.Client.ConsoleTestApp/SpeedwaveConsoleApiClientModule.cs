using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HDHDC.Speedwave.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(SpeedwaveHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SpeedwaveConsoleApiClientModule : AbpModule
    {
        
    }
}
