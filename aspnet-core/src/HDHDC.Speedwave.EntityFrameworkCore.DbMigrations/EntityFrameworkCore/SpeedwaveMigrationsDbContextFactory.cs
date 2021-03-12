using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HDHDC.Speedwave.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class SpeedwaveMigrationsDbContextFactory : IDesignTimeDbContextFactory<SpeedwaveMigrationsDbContext>
    {
        public SpeedwaveMigrationsDbContext CreateDbContext(string[] args)
        {
            SpeedwaveEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SpeedwaveMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new SpeedwaveMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
