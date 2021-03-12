using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Changed_min_required_time_column_in_min_required_time_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinRequiredTime",
                schema: "Speedy",
                table: "MinRequiredTimeEntity");

            migrationBuilder.AddColumn<long>(
                name: "MinRequiredTime",
                schema: "Speedy",
                table: "MinRequiredTimeEntity",
                type: "bigint",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinRequiredTime",
                schema: "Speedy",
                table: "MinRequiredTimeEntity");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MinRequiredTime",
                schema: "Speedy",
                table: "MinRequiredTimeEntity",
                type: "time",
                nullable: false);
        }
    }
}
