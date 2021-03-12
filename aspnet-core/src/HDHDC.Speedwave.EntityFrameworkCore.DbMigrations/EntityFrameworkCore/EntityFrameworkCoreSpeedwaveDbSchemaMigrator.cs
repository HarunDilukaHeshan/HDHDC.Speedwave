using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HDHDC.Speedwave.Data;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.EntityFrameworkCore
{
    public class EntityFrameworkCoreSpeedwaveDbSchemaMigrator
        : ISpeedwaveDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreSpeedwaveDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the SpeedwaveMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<SpeedwaveMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}