using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Speedwave_initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Speedy");

            migrationBuilder.CreateTable(
                name: "CategoryEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 48, nullable: false),
                    CategoryDescription = table.Column<string>(maxLength: 512, nullable: false),
                    CategoryThumbnail = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryScheduleEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryScheduleName = table.Column<string>(maxLength: 48, nullable: false),
                    TimePeriod = table.Column<TimeSpan>(nullable: false),
                    CostIncreasePercentage = table.Column<float>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryScheduleEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistanceChargeEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Charge = table.Column<float>(nullable: false),
                    From = table.Column<long>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistanceChargeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinRequiredTimeEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinRequiredTime = table.Column<TimeSpan>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinRequiredTimeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatusEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatusEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatusEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatusEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsOneTime = table.Column<bool>(nullable: false),
                    NoOfTimes = table.Column<long>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StoreChainEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreChainName = table.Column<string>(maxLength: 48, nullable: false),
                    StoreChainDescription = table.Column<string>(maxLength: 512, nullable: false),
                    StoreChainLogo = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreChainEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubtotalPercentageEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Percentage = table.Column<long>(nullable: false),
                    From = table.Column<float>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtotalPercentageEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UnitSymbol = table.Column<string>(maxLength: 6, nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ProvinceID = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DistrictEntity_ProvinceEntity_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "Speedy",
                        principalTable: "ProvinceEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerEntity_StatusEntity_Status",
                        column: x => x.Status,
                        principalSchema: "Speedy",
                        principalTable: "StatusEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerEntity_AbpUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryChargeEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistanceChargeID = table.Column<int>(nullable: false),
                    SubtotalPercentageID = table.Column<int>(nullable: false),
                    DeliveryScheduleID = table.Column<int>(nullable: false),
                    Charge = table.Column<float>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryChargeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryChargeEntity_DeliveryScheduleEntity_DeliveryScheduleID",
                        column: x => x.DeliveryScheduleID,
                        principalSchema: "Speedy",
                        principalTable: "DeliveryScheduleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChargeEntity_DistanceChargeEntity_DistanceChargeID",
                        column: x => x.DistanceChargeID,
                        principalSchema: "Speedy",
                        principalTable: "DistanceChargeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryChargeEntity_SubtotalPercentageEntity_SubtotalPercentageID",
                        column: x => x.SubtotalPercentageID,
                        principalSchema: "Speedy",
                        principalTable: "SubtotalPercentageEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuantityEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<float>(nullable: false),
                    UnitID = table.Column<string>(nullable: false),
                    NormalizedQuantityLabel = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuantityEntity_UnitEntity_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "Speedy",
                        principalTable: "UnitEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(maxLength: 48, nullable: false),
                    Geolocation = table.Column<string>(nullable: false),
                    DistrictID = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityEntity_DistrictEntity_DistrictID",
                        column: x => x.DistrictID,
                        principalSchema: "Speedy",
                        principalTable: "DistrictEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagerEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(nullable: false),
                    DistrictID = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagerEntity_DistrictEntity_DistrictID",
                        column: x => x.DistrictID,
                        principalSchema: "Speedy",
                        principalTable: "DistrictEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagerEntity_StatusEntity_Status",
                        column: x => x.Status,
                        principalSchema: "Speedy",
                        principalTable: "StatusEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagerEntity_AbpUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(maxLength: 48, nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 512, nullable: false),
                    ItemPrice = table.Column<float>(nullable: false),
                    ItemThumbnail = table.Column<string>(nullable: false),
                    QuantityId = table.Column<int>(nullable: false),
                    MinRequiredTimeId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemEntity_MinRequiredTimeEntity_MinRequiredTimeId",
                        column: x => x.MinRequiredTimeId,
                        principalSchema: "Speedy",
                        principalTable: "MinRequiredTimeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemEntity_QuantityEntity_QuantityId",
                        column: x => x.QuantityId,
                        principalSchema: "Speedy",
                        principalTable: "QuantityEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine = table.Column<string>(maxLength: 128, nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false),
                    Geolocation = table.Column<string>(nullable: false),
                    Note = table.Column<string>(maxLength: 512, nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressEntity_CityEntity_CityID",
                        column: x => x.CityID,
                        principalSchema: "Speedy",
                        principalTable: "CityEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressEntity_CustomerEntity_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "Speedy",
                        principalTable: "CustomerEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiderEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(nullable: false),
                    Geolocation = table.Column<string>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiderEntity_CityEntity_CityID",
                        column: x => x.CityID,
                        principalSchema: "Speedy",
                        principalTable: "CityEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiderEntity_StatusEntity_Status",
                        column: x => x.Status,
                        principalSchema: "Speedy",
                        principalTable: "StatusEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiderEntity_AbpUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreBranchEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreChainID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    Geolocation = table.Column<string>(nullable: false),
                    ContactNo01 = table.Column<string>(nullable: false),
                    ContactNo02 = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreBranchEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreBranchEntity_CityEntity_CityID",
                        column: x => x.CityID,
                        principalSchema: "Speedy",
                        principalTable: "CityEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreBranchEntity_StoreChainEntity_StoreChainID",
                        column: x => x.StoreChainID,
                        principalSchema: "Speedy",
                        principalTable: "StoreChainEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategoryEntity",
                schema: "Speedy",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ItemEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategoryEntity", x => new { x.ItemID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_ItemCategoryEntity_CategoryEntity_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "Speedy",
                        principalTable: "CategoryEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemCategoryEntity_ItemEntity_ItemEntityId",
                        column: x => x.ItemEntityId,
                        principalSchema: "Speedy",
                        principalTable: "ItemEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemCategoryEntity_ItemEntity_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Speedy",
                        principalTable: "ItemEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiderCoverageEntity",
                schema: "Speedy",
                columns: table => new
                {
                    RiderID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderCoverageEntity", x => new { x.RiderID, x.CityID });
                    table.ForeignKey(
                        name: "FK_RiderCoverageEntity_CityEntity_CityID",
                        column: x => x.CityID,
                        principalSchema: "Speedy",
                        principalTable: "CityEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiderCoverageEntity_RiderEntity_RiderID",
                        column: x => x.RiderID,
                        principalSchema: "Speedy",
                        principalTable: "RiderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemStoreBranchEntity",
                schema: "Speedy",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false),
                    StoreBranchID = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    ItemEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemStoreBranchEntity", x => new { x.ItemID, x.StoreBranchID });
                    table.ForeignKey(
                        name: "FK_ItemStoreBranchEntity_ItemEntity_ItemEntityId",
                        column: x => x.ItemEntityId,
                        principalSchema: "Speedy",
                        principalTable: "ItemEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemStoreBranchEntity_ItemEntity_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Speedy",
                        principalTable: "ItemEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemStoreBranchEntity_StoreBranchEntity_StoreBranchID",
                        column: x => x.StoreBranchID,
                        principalSchema: "Speedy",
                        principalTable: "StoreBranchEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreClosingDateEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreBranchID = table.Column<int>(nullable: false),
                    ClosingDate = table.Column<DateTime>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreClosingDateEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreClosingDateEntity_StoreBranchEntity_StoreBranchID",
                        column: x => x.StoreBranchID,
                        principalSchema: "Speedy",
                        principalTable: "StoreBranchEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreOpenDayEntity",
                schema: "Speedy",
                columns: table => new
                {
                    StoreBranchID = table.Column<int>(nullable: false),
                    DayOfWeek = table.Column<int>(nullable: false),
                    OpeningTime = table.Column<TimeSpan>(nullable: false),
                    ClosingTime = table.Column<TimeSpan>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOpenDayEntity", x => new { x.StoreBranchID, x.DayOfWeek });
                    table.ForeignKey(
                        name: "FK_StoreOpenDayEntity_StoreBranchEntity_StoreBranchID",
                        column: x => x.StoreBranchID,
                        principalSchema: "Speedy",
                        principalTable: "StoreBranchEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryScheduleID = table.Column<int>(nullable: false),
                    AddressID = table.Column<int>(nullable: false),
                    PromotionID = table.Column<int>(nullable: true),
                    OrderStatus = table.Column<string>(nullable: false),
                    PaymentEntityId1 = table.Column<int>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderEntity_AddressEntity_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "Speedy",
                        principalTable: "AddressEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEntity_DeliveryScheduleEntity_DeliveryScheduleID",
                        column: x => x.DeliveryScheduleID,
                        principalSchema: "Speedy",
                        principalTable: "DeliveryScheduleEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEntity_OrderStatusEntity_OrderStatus",
                        column: x => x.OrderStatus,
                        principalSchema: "Speedy",
                        principalTable: "OrderStatusEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderEntity_PromotionEntity_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "Speedy",
                        principalTable: "PromotionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemEntity",
                schema: "Speedy",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    ItemPrice = table.Column<float>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemEntity", x => new { x.ItemID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_OrderItemEntity_ItemEntity_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Speedy",
                        principalTable: "ItemEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItemEntity_OrderEntity_OrderID",
                        column: x => x.OrderID,
                        principalSchema: "Speedy",
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryChargeID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    Nettotal = table.Column<float>(nullable: false),
                    Subtotal = table.Column<float>(nullable: false),
                    TotalPaid = table.Column<float>(nullable: false),
                    PaymentStatus = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentEntity_DeliveryChargeEntity_DeliveryChargeID",
                        column: x => x.DeliveryChargeID,
                        principalSchema: "Speedy",
                        principalTable: "DeliveryChargeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentEntity_OrderEntity_OrderID",
                        column: x => x.OrderID,
                        principalSchema: "Speedy",
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentEntity_PaymentStatusEntity_PaymentStatus",
                        column: x => x.PaymentStatus,
                        principalSchema: "Speedy",
                        principalTable: "PaymentStatusEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDetailEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentID = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: false),
                    TotalPaid = table.Column<float>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    PaymentEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetailEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentDetailEntity_PaymentEntity_PaymentEntityId",
                        column: x => x.PaymentEntityId,
                        principalSchema: "Speedy",
                        principalTable: "PaymentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDetailEntity_PaymentEntity_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "Speedy",
                        principalTable: "PaymentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDetailEntity_PaymentMethodEntity_PaymentMethod",
                        column: x => x.PaymentMethod,
                        principalSchema: "Speedy",
                        principalTable: "PaymentMethodEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_CityID",
                schema: "Speedy",
                table: "AddressEntity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_CustomerID",
                schema: "Speedy",
                table: "AddressEntity",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CityEntity_DistrictID",
                schema: "Speedy",
                table: "CityEntity",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEntity_Status",
                schema: "Speedy",
                table: "CustomerEntity",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEntity_UserID",
                schema: "Speedy",
                table: "CustomerEntity",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChargeEntity_DeliveryScheduleID",
                schema: "Speedy",
                table: "DeliveryChargeEntity",
                column: "DeliveryScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChargeEntity_DistanceChargeID",
                schema: "Speedy",
                table: "DeliveryChargeEntity",
                column: "DistanceChargeID");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryChargeEntity_SubtotalPercentageID",
                schema: "Speedy",
                table: "DeliveryChargeEntity",
                column: "SubtotalPercentageID");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictEntity_ProvinceID",
                schema: "Speedy",
                table: "DistrictEntity",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategoryEntity_CategoryID",
                schema: "Speedy",
                table: "ItemCategoryEntity",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategoryEntity_ItemEntityId",
                schema: "Speedy",
                table: "ItemCategoryEntity",
                column: "ItemEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEntity_MinRequiredTimeId",
                schema: "Speedy",
                table: "ItemEntity",
                column: "MinRequiredTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemEntity_QuantityId",
                schema: "Speedy",
                table: "ItemEntity",
                column: "QuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStoreBranchEntity_ItemEntityId",
                schema: "Speedy",
                table: "ItemStoreBranchEntity",
                column: "ItemEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemStoreBranchEntity_StoreBranchID",
                schema: "Speedy",
                table: "ItemStoreBranchEntity",
                column: "StoreBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerEntity_DistrictID",
                schema: "Speedy",
                table: "ManagerEntity",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerEntity_Status",
                schema: "Speedy",
                table: "ManagerEntity",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerEntity_UserID",
                schema: "Speedy",
                table: "ManagerEntity",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_AddressID",
                schema: "Speedy",
                table: "OrderEntity",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_DeliveryScheduleID",
                schema: "Speedy",
                table: "OrderEntity",
                column: "DeliveryScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_OrderStatus",
                schema: "Speedy",
                table: "OrderEntity",
                column: "OrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity",
                column: "PaymentEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_PromotionID",
                schema: "Speedy",
                table: "OrderEntity",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemEntity_OrderID",
                schema: "Speedy",
                table: "OrderItemEntity",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetailEntity_PaymentEntityId",
                schema: "Speedy",
                table: "PaymentDetailEntity",
                column: "PaymentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetailEntity_PaymentID",
                schema: "Speedy",
                table: "PaymentDetailEntity",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDetailEntity_PaymentMethod",
                schema: "Speedy",
                table: "PaymentDetailEntity",
                column: "PaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntity_DeliveryChargeID",
                schema: "Speedy",
                table: "PaymentEntity",
                column: "DeliveryChargeID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntity_OrderID",
                schema: "Speedy",
                table: "PaymentEntity",
                column: "OrderID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEntity_PaymentStatus",
                schema: "Speedy",
                table: "PaymentEntity",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityEntity_UnitID",
                schema: "Speedy",
                table: "QuantityEntity",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_RiderCoverageEntity_CityID",
                schema: "Speedy",
                table: "RiderCoverageEntity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_RiderEntity_CityID",
                schema: "Speedy",
                table: "RiderEntity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_RiderEntity_Status",
                schema: "Speedy",
                table: "RiderEntity",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_RiderEntity_UserID",
                schema: "Speedy",
                table: "RiderEntity",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchEntity_CityID",
                schema: "Speedy",
                table: "StoreBranchEntity",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreBranchEntity_StoreChainID",
                schema: "Speedy",
                table: "StoreBranchEntity",
                column: "StoreChainID");

            migrationBuilder.CreateIndex(
                name: "IX_StoreClosingDateEntity_StoreBranchID",
                schema: "Speedy",
                table: "StoreClosingDateEntity",
                column: "StoreBranchID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_PaymentEntity_PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity",
                column: "PaymentEntityId1",
                principalSchema: "Speedy",
                principalTable: "PaymentEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressEntity_CityEntity_CityID",
                schema: "Speedy",
                table: "AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressEntity_CustomerEntity_CustomerID",
                schema: "Speedy",
                table: "AddressEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChargeEntity_DeliveryScheduleEntity_DeliveryScheduleID",
                schema: "Speedy",
                table: "DeliveryChargeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_DeliveryScheduleEntity_DeliveryScheduleID",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChargeEntity_DistanceChargeEntity_DistanceChargeID",
                schema: "Speedy",
                table: "DeliveryChargeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryChargeEntity_SubtotalPercentageEntity_SubtotalPercentageID",
                schema: "Speedy",
                table: "DeliveryChargeEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_AddressEntity_AddressID",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_OrderStatusEntity_OrderStatus",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_PaymentEntity_PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropTable(
                name: "ItemCategoryEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "ItemStoreBranchEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "ManagerEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "OrderItemEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "PaymentDetailEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "RiderCoverageEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "StoreClosingDateEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "StoreOpenDayEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "CategoryEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "ItemEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "PaymentMethodEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "RiderEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "StoreBranchEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "MinRequiredTimeEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "QuantityEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "StoreChainEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "UnitEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "CityEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "DistrictEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "ProvinceEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "CustomerEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "StatusEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "DeliveryScheduleEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "DistanceChargeEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "SubtotalPercentageEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "AddressEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "OrderStatusEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "PaymentEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "DeliveryChargeEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "OrderEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "PaymentStatusEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "PromotionEntity",
                schema: "Speedy");
        }
    }
}
