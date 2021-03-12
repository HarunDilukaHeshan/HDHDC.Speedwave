using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Search_for_items_modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"" +
                "ALTER FUNCTION [Speedy].[searchForItems]( " +
                "@keywords VARCHAR(MAX), " +
                "@skipCount INT = 0, " +
                "@maxResultCount INT = 10) " +
                "RETURNS @table TABLE(Id INT UNIQUE) " +
                "AS " +
                "BEGIN " +
                    "INSERT INTO @table SELECT DISTINCT itemE.Id FROM [Speedy].[ItemEntity] AS itemE " +
                    "INNER JOIN [Speedy].[ItemCategoryEntity] AS itemCatE ON itemE.Id = itemCatE.ItemID " +
                    "INNER JOIN [Speedy].[CategoryEntity] AS catE ON itemCatE.CategoryID = catE.Id " +
                    "WHERE itemE.ItemName LIKE '%' + @keywords + '%' OR " +
                        "itemE.ItemDescription LIKE '%' + @keywords + '%' OR " +
                        "catE.CategoryName LIKE '%' + @keywords + '%' OR " +
                        "catE.CategoryDescription LIKE '%' + @keywords + '%' " +
                        "ORDER BY itemE.Id " +
                        "OFFSET @skipCount ROWS " +
                        "FETCH NEXT @maxResultCount ROWS ONLY " +
                        "RETURN; " +
                "END; ";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"" +
                "ALTER FUNCTION [Speedy].[searchForItems]( " +
                "@keywords VARCHAR(MAX), " +
                "@skipCount INT = 0, " +
                "@maxResultCount INT = 10) " +
                "RETURNS @table TABLE(Id INT UNIQUE) " +
                "AS " +
                "BEGIN " +
                    "INSERT INTO @table SELECT itemE.Id FROM [Speedy].[ItemEntity] AS itemE " +
                    "WHERE itemE.ItemName LIKE '%' + @keywords + '%' OR " +
                        "itemE.ItemDescription LIKE '%' + @keywords + '%' " +
                        "ORDER BY itemE.Id " +
                        "OFFSET @skipCount ROWS " +
                        "FETCH NEXT @maxResultCount ROWS ONLY " +
                        "RETURN; " +
                "END; ";

            migrationBuilder.Sql(sql);
        }
    }
}
