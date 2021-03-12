using HDHDC.Speedwave.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HDHDC.Speedwave
{
    [DependsOn(
        typeof(SpeedwaveEntityFrameworkCoreTestModule)
        )]
    public class SpeedwaveDomainTestModule : AbpModule
    {

    }
}