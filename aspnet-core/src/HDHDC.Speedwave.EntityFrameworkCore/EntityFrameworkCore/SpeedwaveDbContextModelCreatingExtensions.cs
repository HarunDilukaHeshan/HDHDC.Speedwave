using HDHDC.Speedwave.SpeedwaveEntityCollection;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using HDHDC.Speedwave.SpeedyConsts;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using System;
using HDHDC.Speedwave.Users;

namespace HDHDC.Speedwave.EntityFrameworkCore
{
    public static class SpeedwaveDbContextModelCreatingExtensions
    {
        public static void ConfigureSpeedwave(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(SpeedwaveConsts.DbTablePrefix + "YourEntities", SpeedwaveConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});            

            // Uncomment following two lines when dealing with migrations

           // builder.Entity<ManagerEntity>().Ignore(e => e.AppUser);
           // builder.Entity<RiderEntity>().Ignore(e => e.AppUser);

            // Query types (Keyless types)
            builder.Entity<RiderMeanDistanceKeylessEntity>().HasNoKey();


            builder.Entity<AddressEntity>(b =>
            {
                b.ToTable("AddressEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne(e => e.CityEntity)
                .WithMany()
                .HasForeignKey(e => e.CityID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.CustomerEntity)
                .WithMany()
                .HasForeignKey(e => e.CustomerID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.Property(e => e.AddressLine)
                .HasMaxLength(EntityConstraintsConsts.AddressMaxLength)
                .IsRequired();

                b.Property(e => e.Geolocation)
                .IsRequired();

                b.Property(e => e.Note)
                .HasMaxLength(EntityConstraintsConsts.DescriptionMaxLength);
            });

            builder.Entity<CategoryEntity>(b =>
            {
                b.ToTable("CategoryEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.CategoryName)
                .HasMaxLength(EntityConstraintsConsts.NameMaxLength)
                .IsRequired();

                b.Property(e => e.CategoryDescription)
                .HasMaxLength(EntityConstraintsConsts.DescriptionMaxLength)
                .IsRequired();

                b.Property(e => e.CategoryThumbnail)
                .IsRequired();
            });

            builder.Entity<CityEntity>(b =>
            {
                b.ToTable("CityEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.CityName)
                .HasMaxLength(EntityConstraintsConsts.NameMaxLength)
                .IsRequired();

                b.Property(e => e.Geolocation)
                .IsRequired();

                b.HasOne(e => e.DistrictEntity)
                .WithMany()
                .HasForeignKey(e => e.DistrictID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CustomerEntity>(b =>
            {
                b.ToTable("CustomerEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                //b.HasOne<IdentityUser>()
                //.WithOne()
                //.HasForeignKey<CustomerEntity>(e => e.UserID)
                //.IsRequired()
                //.OnDelete(DeleteBehavior.Restrict);

                b.HasOne<StatusEntity>()
                .WithMany()
                .HasForeignKey(e => e.Status)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CancelledOrderEntity>(b => {
                b.ToTable("CancelledOrderEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.Description)
                .HasMaxLength(EntityConstraintsConsts.DescriptionMaxLength);

                b.HasOne(e => e.OrderEntity)
                .WithOne()
                .HasForeignKey<CancelledOrderEntity>(e => e.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.CancellationReasonEntity)
                .WithMany()
                .HasForeignKey(e => e.CancellationReasonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.ManagerEntity)
                .WithMany()
                .HasForeignKey(e => e.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CancellationReasonEntity>(b => {
                b.ToTable("CancellationReasonEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.CancellationReason)
                .HasMaxLength(EntityConstraintsConsts.NameMaxLength)
                .IsRequired();

                b.Property(e => e.Description)
                .HasMaxLength(EntityConstraintsConsts.DescriptionMaxLength)
                .IsRequired();
            });

            builder.Entity<DeliveryChargeEntity>(b =>
            {
                b.ToTable("DeliveryChargeEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne(e => e.DistanceChargeEntity)
                .WithMany()
                .HasForeignKey(e => e.DistanceChargeID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeliveryScheduleEntity)
                .WithMany()
                .HasForeignKey(e => e.DeliveryScheduleID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.SubtotalPercentageEntity)
                .WithMany()
                .HasForeignKey(e => e.SubtotalPercentageID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<DeliveryScheduleEntity>(b =>
            {
                b.ToTable("DeliveryScheduleEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.DeliveryScheduleName)
                .HasMaxLength(EntityConstraintsConsts.NameMaxLength)
                .IsRequired();

                b.Property(e => e.TimePeriod)
                .HasConversion(ts => ts.Ticks / 10000, pt => TimeSpan.FromTicks(pt * 10000))
                .IsRequired();
            });

            builder.Entity<DistanceChargeEntity>(b =>
            {
                b.ToTable("DistanceChargeEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<DistrictEntity>(b =>
            {
                b.ToTable("DistrictEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne<ProvinceEntity>()
                .WithMany()
                .HasForeignKey(e => e.ProvinceID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ItemCategoryEntity>(b =>
            {
                b.ToTable("ItemCategoryEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasKey(e => new { e.ItemID, e.CategoryID });

                b.HasOne(e => e.ItemEntity)
                .WithMany()
                .HasForeignKey(e => e.ItemID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.CategoryEntity)
                .WithMany()
                .HasForeignKey(e => e.CategoryID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ItemEntity>(b =>
            {
                b.ToTable("ItemEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.ItemName)
                .HasMaxLength(EntityConstraintsConsts.NameMaxLength)
                .IsRequired();

                b.Property(e => e.ItemDescription)
                .HasMaxLength(EntityConstraintsConsts.DescriptionMaxLength)
                .IsRequired();

                b.Property(e => e.ItemThumbnail)
                .IsRequired();

                b.HasOne(e => e.MinRequiredTimeEntity)
                .WithMany()
                .HasForeignKey(e => e.MinRequiredTimeId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.QuantityEntity)
                .WithMany()
                .HasForeignKey(e => e.QuantityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.Property(e => e.NormalizedQuantityLabel)                
                .HasComputedColumnSql("Speedy.Get_normalized_quantity(QuantityId)");                
            });

            builder.Entity<ItemStoreBranchEntity>(b =>
            {
                b.ToTable("ItemStoreBranchEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasKey(e => new { e.ItemID, e.StoreBranchID });

                b.HasOne(e => e.ItemEntity)
                .WithMany()
                .HasForeignKey(e => e.ItemID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.StoreBranchEntity)
                .WithMany()
                .HasForeignKey(e => e.StoreBranchID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ManagerEntity>(b =>
            {
                b.ToTable("ManagerEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne(e => e.DistrictEntity)
                .WithMany()
                .HasForeignKey(e => e.DistrictID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);                

                b.HasOne<StatusEntity>()
                .WithMany()
                .HasForeignKey(e => e.Status)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<MinRequiredTimeEntity>(b => {
                b.ToTable("MinRequiredTimeEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.MinRequiredTime)
                .HasConversion(ts => ts.Ticks, pt => TimeSpan.FromTicks(pt))
                .IsRequired();
            });

            builder.Entity<OrderEntity>(b =>
            {
                b.ToTable("OrderEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne(e => e.RiderEntity)
                .WithMany()
                .HasForeignKey(e => e.RiderID)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeliveryScheduleEntity)
                .WithMany()
                .HasForeignKey(e => e.DeliveryScheduleID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.PromotionEntity)
                .WithMany()
                .HasForeignKey(e => e.PromotionID)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.AddressEntity)
                .WithMany()
                .HasForeignKey(e => e.AddressID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<OrderStatusEntity>()
                .WithMany()
                .HasForeignKey(e => e.OrderStatus)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<OrderItemEntity>(b =>
            {
                b.ToTable("OrderItemEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasKey(e => new { e.ItemID, e.OrderID });

                b.HasOne(e => e.ItemEntity)
                .WithMany()
                .HasForeignKey(e => e.ItemID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.OrderEntity)
                .WithMany(e => e.OrderItemEntities)
                .HasForeignKey(e => e.OrderID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<OrderStatusEntity>(b =>
            {
                b.ToTable("OrderStatusEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<PaymentDetailEntity>(b =>
            {
                b.ToTable("PaymentDetailEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne(e => e.PaymentEntity)
                .WithMany()
                .HasForeignKey(e => e.PaymentID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<PaymentMethodEntity>()
                .WithMany()
                .HasForeignKey(e => e.PaymentMethod)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PaymentEntity>(b =>
            {
                b.ToTable("PaymentEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne(e => e.OrderEntity)
                .WithOne(e => e.PaymentEntity)
                .HasForeignKey<PaymentEntity>(e => e.OrderID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.DeliveryChargeEntity)
                .WithMany()
                .HasForeignKey(e => e.DeliveryChargeID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<PaymentStatusEntity>()
                .WithMany()
                .HasForeignKey(e => e.PaymentStatus)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PaymentMethodEntity>(b =>
            {
                b.ToTable("PaymentMethodEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<PaymentStatusEntity>(b =>
            {
                b.ToTable("PaymentStatusEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<PromotionEntity>(b =>
            {
                b.ToTable("PromotionEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.ExpireDate)
                .IsRequired();
            });

            builder.Entity<ProvinceEntity>(b =>
            {
                b.ToTable("ProvinceEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<QuantityEntity>(b =>
            {
                b.ToTable("QuantityEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasOne<UnitEntity>()
                .WithMany()
                .HasForeignKey(e => e.UnitID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.Property(e => e.NormalizedQuantityLabel)
                .IsRequired();
            });

            builder.Entity<RiderCoverageEntity>(b =>
            {
                b.ToTable("RiderCoverageEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasKey(e => new { e.RiderID, e.CityID });

                b.HasOne(e => e.RiderEntity)
                .WithMany()
                .HasForeignKey(e => e.RiderID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.CityEntity)
                .WithMany()
                .HasForeignKey(e => e.CityID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RiderEntity>(b =>
            {
                b.ToTable("RiderEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.Geolocation)
                .IsRequired();                

                b.HasOne(e => e.CityEntity)
                .WithMany()
                .HasForeignKey(e => e.CityID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                //b.HasOne<IdentityUser>()
                //.WithOne()
                //.HasForeignKey<RiderEntity>(e => e.UserID)
                //.IsRequired()
                //.OnDelete(DeleteBehavior.Restrict);

                b.HasOne<StatusEntity>()
                .WithMany()
                .HasForeignKey(e => e.Status)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<StatusEntity>(b =>
            {
                b.ToTable("StatusEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<StoreBranchEntity>(b =>
            {
                b.ToTable("StoreBranchEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.Geolocation)
                .IsRequired();

                b.Property(e => e.ContactNo01)
                .IsRequired();

                b.HasOne(e => e.StoreChainEntity)
                .WithMany()
                .HasForeignKey(e => e.StoreChainID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.CityEntity)
                .WithMany()
                .HasForeignKey(e => e.CityID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<StoreChainEntity>(b =>
            {
                b.ToTable("StoreChainEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.StoreChainName)
                .HasMaxLength(EntityConstraintsConsts.NameMaxLength)
                .IsRequired();

                b.Property(e => e.StoreChainDescription)
                .HasMaxLength(EntityConstraintsConsts.DescriptionMaxLength)
                .IsRequired();

                b.Property(e => e.StoreChainLogo)
                .IsRequired();
            });


            builder.Entity<StoreClosingDateEntity>(b =>
            {
                b.ToTable("StoreClosingDateEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.ClosingDate)
                .IsRequired();

                b.HasOne(e => e.StoreBranchEntity)
                .WithMany()
                .HasForeignKey(e => e.StoreBranchID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<StoreOpenDayEntity>(b =>
            {
                b.ToTable("StoreOpenDayEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.HasKey(e => new { e.StoreBranchID, e.DayOfWeek });

                b.Property(e => e.DayOfWeek)
                .IsRequired();

                b.Property(e => e.OpeningTime)
                .IsRequired();

                b.Property(e => e.ClosingTime)
                .IsRequired();

                b.HasOne(e => e.StoreBranchEntity)
                .WithMany()
                .HasForeignKey(e => e.StoreBranchID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<SubtotalPercentageEntity>(b =>
            {
                b.ToTable("SubtotalPercentageEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<UnitEntity>(b =>
            {
                b.ToTable("UnitEntity", SpeedwaveConsts.SpeedyDbSchema);
                b.ConfigureByConvention();

                b.Property(e => e.UnitSymbol)
                .HasMaxLength(EntityConstraintsConsts.UnitSymbolMaxLength)
                .IsRequired();
            });
        }
    }
}