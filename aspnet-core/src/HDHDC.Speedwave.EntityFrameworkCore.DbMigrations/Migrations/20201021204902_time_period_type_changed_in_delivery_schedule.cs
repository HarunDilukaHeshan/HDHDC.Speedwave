using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class time_period_type_changed_in_delivery_schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePeriod",
                schema: "Speedy",
                table: "DeliveryScheduleEntity");

            migrationBuilder.AddColumn<long>(
                name: "TimePeriod",
                schema: "Speedy",
                table: "DeliveryScheduleEntity",
                type: "bigint",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePeriod",
                schema: "Speedy",
                table: "DeliveryScheduleEntity");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimePeriod",
                schema: "Speedy",
                table: "DeliveryScheduleEntity",
                type: "bigint",
                nullable: false);
        }
    }
}
