using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Added_computed_column_normalized_quantity_label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedQuantityLabel",
                schema: "Speedy",
                table: "ItemEntity",
                nullable: true,
                computedColumnSql: "Speedy.Get_normalized_quantity(QuantityId)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedQuantityLabel",
                schema: "Speedy",
                table: "ItemEntity");
        }
    }
}
