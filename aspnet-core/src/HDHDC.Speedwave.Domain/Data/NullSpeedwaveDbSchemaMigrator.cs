using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.Data
{
    /* This is used if database provider does't define
     * ISpeedwaveDbSchemaMigrator implementation.
     */
    public class NullSpeedwaveDbSchemaMigrator : ISpeedwaveDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}