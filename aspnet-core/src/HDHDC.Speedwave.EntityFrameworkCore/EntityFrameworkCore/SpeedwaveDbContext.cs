using Microsoft.EntityFrameworkCore;
using HDHDC.Speedwave.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using HDHDC.Speedwave.SpeedwaveEntityCollection;

namespace HDHDC.Speedwave.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See SpeedwaveMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class SpeedwaveDbContext : AbpDbContext<SpeedwaveDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        #region speedwave_entities

        public DbSet<CancelledOrderEntity> CancelledOrderEntities { get; set; }
        public DbSet<CancellationReasonEntity> CancellationReasonEntities { get; set; }
        public DbSet<RiderMeanDistanceKeylessEntity> MeanDistance { get; set; }
        public DbSet<AddressEntity> AddressEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<CityEntity> CityEntities { get; set; }
        public DbSet<CustomerEntity> CustomerEntities { get; set; }
        public DbSet<DeliveryChargeEntity> DeliveryChargeEntities { get; set; }
        public DbSet<DeliveryScheduleEntity> DeliveryScheduleEntities { get; set; }
        public DbSet<DistanceChargeEntity> DistanceChargeEntities { get; set; }
        public DbSet<DistrictEntity> DistrictEntities { get; set; }
        public DbSet<ItemCategoryEntity> ItemCategoryEntities { get; set; }
        public DbSet<ItemEntity> ItemEntities { get; set; }
        public DbSet<ItemStoreBranchEntity> ItemStoreBranchEntities { get; set; }
        public DbSet<ManagerEntity> ManagerEntities { get; set; }
        public DbSet<MinRequiredTimeEntity> MinRequiredTimeEntities { get; set; }
        public DbSet<OrderEntity> OrderEntities { get; set; }
        public DbSet<OrderItemEntity> OrderItemEntities { get; set; }
        public DbSet<OrderStatusEntity> OrderStatusEntities { get; set; }
        public DbSet<PaymentDetailEntity> PaymentDetailEntities { get; set; }
        public DbSet<PaymentEntity> PaymentEntities { get; set; }
        public DbSet<PaymentMethodEntity> PaymentMethodEntities { get; set; }
        public DbSet<PaymentStatusEntity> PaymentStatusEntities { get; set; }
        public DbSet<PromotionEntity> PromotionEntities { get; set; }
        public DbSet<ProvinceEntity> ProvinceEntities { get; set; }
        public DbSet<QuantityEntity> QuantityEntities { get; set; }
        public DbSet<RiderCoverageEntity> RiderCoverageEntities { get; set; }
        public DbSet<RiderEntity> RiderEntities { get; set; }
        public DbSet<StatusEntity> StatusEntities { get; set; }
        public DbSet<StoreBranchEntity> StoreBranchEntities { get; set; }
        public DbSet<StoreChainEntity> StoreChainEntities { get; set; }
        public DbSet<StoreClosingDateEntity> StoreClosingDateEntities { get; set; }
        public DbSet<StoreOpenDayEntity> StoreOpenDayEntities { get; set; }
        public DbSet<SubtotalPercentageEntity> SubtotalPercentageEntities { get; set; }
        public DbSet<UnitEntity> UnitEntities { get; set; }
        #endregion

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside SpeedwaveDbContextModelCreatingExtensions.ConfigureSpeedwave
         */

        public SpeedwaveDbContext(DbContextOptions<SpeedwaveDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
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
                .WithOne(e => e.AppUser)
                .HasForeignKey<ManagerEntity>(e => e.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<RiderEntity>()
                .WithOne(e => e.AppUser)
                .HasForeignKey<RiderEntity>(e => e.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                /* Configure mappings for your additional properties
                 * Also see the SpeedwaveEfCoreEntityExtensionMappings class
                 */
            });            

            /* Configure your own tables/entities inside the ConfigureSpeedwave method */

            builder.ConfigureSpeedwave();
        }
    }
}
