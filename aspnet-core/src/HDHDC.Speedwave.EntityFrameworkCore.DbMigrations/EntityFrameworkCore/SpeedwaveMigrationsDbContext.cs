using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace HDHDC.Speedwave.EntityFrameworkCore
{
    /* This DbContext is only used for database migrations.
     * It is not used on runtime. See SpeedwaveDbContext for the runtime DbContext.
     * It is a unified model that includes configuration for
     * all used modules and your application.
     */
    public class SpeedwaveMigrationsDbContext : AbpDbContext<SpeedwaveMigrationsDbContext>
    {
        public SpeedwaveMigrationsDbContext(DbContextOptions<SpeedwaveMigrationsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside the ConfigureSpeedwave method */

            builder.Entity<IdentityUser>(b =>
            {

                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                b.HasOne<CustomerEntity>()
                .WithOne()
                .HasForeignKey<CustomerEntity>(e => e.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<ManagerEntity>()
                .WithOne()
                .HasForeignKey<ManagerEntity>(e => e.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<RiderEntity>()
                .WithOne()
                .HasForeignKey<RiderEntity>(e => e.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.ConfigureSpeedwave();
        }
    }
}