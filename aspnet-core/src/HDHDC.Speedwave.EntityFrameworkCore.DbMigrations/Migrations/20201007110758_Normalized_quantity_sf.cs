using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Normalized_quantity_sf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
                CREATE FUNCTION Speedy.Get_normalized_quantity(@quantityId AS INT)
                RETURNS VARCHAR(50)
                AS
                BEGIN
                    DECLARE @normalizedQuantity AS VARCHAR(50)
                    SET @normalizedQuantity = (SELECT NormalizedQuantityLabel FROM Speedy.QuantityEntity WHERE Id = @quantityId)
                    RETURN @normalizedQuantity
                END;
            ";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Speedy.Get_normalized_quantity", true);
        }
    }
}
