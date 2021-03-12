using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Cancelled_order_entity_and_cancellation_reason_entity_and_rider_id_to_order_entity_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_PaymentEntity_PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntity_PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropColumn(
                name: "PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.AddColumn<int>(
                name: "RiderID",
                schema: "Speedy",
                table: "OrderEntity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CancellationReasonEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CancellationReason = table.Column<string>(maxLength: 48, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationReasonEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CancelledOrderEntity",
                schema: "Speedy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CancellationReasonId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApproverId = table.Column<int>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledOrderEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelledOrderEntity_ManagerEntity_ApproverId",
                        column: x => x.ApproverId,
                        principalSchema: "Speedy",
                        principalTable: "ManagerEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelledOrderEntity_CancellationReasonEntity_CancellationReasonId",
                        column: x => x.CancellationReasonId,
                        principalSchema: "Speedy",
                        principalTable: "CancellationReasonEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelledOrderEntity_OrderEntity_Id",
                        column: x => x.Id,
                        principalSchema: "Speedy",
                        principalTable: "OrderEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_RiderID",
                schema: "Speedy",
                table: "OrderEntity",
                column: "RiderID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledOrderEntity_ApproverId",
                schema: "Speedy",
                table: "CancelledOrderEntity",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledOrderEntity_CancellationReasonId",
                schema: "Speedy",
                table: "CancelledOrderEntity",
                column: "CancellationReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntity_RiderEntity_RiderID",
                schema: "Speedy",
                table: "OrderEntity",
                column: "RiderID",
                principalSchema: "Speedy",
                principalTable: "RiderEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntity_RiderEntity_RiderID",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropTable(
                name: "CancelledOrderEntity",
                schema: "Speedy");

            migrationBuilder.DropTable(
                name: "CancellationReasonEntity",
                schema: "Speedy");

            migrationBuilder.DropIndex(
                name: "IX_OrderEntity_RiderID",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.DropColumn(
                name: "RiderID",
                schema: "Speedy",
                table: "OrderEntity");

            migrationBuilder.AddColumn<int>(
                name: "PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderEntity_PaymentEntityId1",
                schema: "Speedy",
                table: "OrderEntity",
                column: "PaymentEntityId1");

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
    }
}
