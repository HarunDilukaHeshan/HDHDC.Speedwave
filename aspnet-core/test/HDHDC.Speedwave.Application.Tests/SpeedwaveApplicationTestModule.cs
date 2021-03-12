using Volo.Abp.Modularity;

namespace HDHDC.Speedwave
{
    [DependsOn(
        typeof(SpeedwaveApplicationModule),
        typeof(SpeedwaveDomainTestModule)
        )]
    public class SpeedwaveApplicationTestModule : AbpModule
    {

    }
}