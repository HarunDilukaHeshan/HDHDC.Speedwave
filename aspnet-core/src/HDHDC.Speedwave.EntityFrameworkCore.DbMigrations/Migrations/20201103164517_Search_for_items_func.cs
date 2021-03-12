using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Search_for_items_func : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var searchForItems = "" +
                "CREATE FUNCTION [Speedy].[searchForItems]( " +
                    "@keywords VARCHAR(MAX), " +
                    "@skipCount INT = 0, " +
                    "@maxResultCount INT = 10) " +
                "RETURNS @table TABLE(Id INT UNIQUE) " +
                "AS " +
                "BEGIN " +
                    "INSERT INTO @table " +
                    "SELECT itemE.Id FROM Speedy.ItemEntity AS itemE " +
                    "WHERE itemE.ItemName LIKE '%' + @keywords + '%' OR " +
                        "itemE.ItemDescription LIKE '%' + @keywords + '%' " +
                    "ORDER BY itemE.Id " +
                    "OFFSET @skipCount ROWS " +
                    "FETCH NEXT @maxResultCount ROWS ONLY " +
                    "RETURN; " +
                "END; ";

            migrationBuilder.Sql(searchForItems);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var searchForItems = "DROP FUNCTION [Speedy].[searchForItems]";
            migrationBuilder.Sql(searchForItems);
        }
    }
}
