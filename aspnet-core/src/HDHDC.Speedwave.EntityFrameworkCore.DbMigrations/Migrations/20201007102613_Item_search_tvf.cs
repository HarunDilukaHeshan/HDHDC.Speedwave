using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Item_search_tvf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlSearchForItemIDsTVF = @"
                CREATE FUNCTION Speedy.SearchForItemIDsTVF (
                    @keywords AS VARCHAR(256) = '',
                    @skipCount AS INT = 0,
                    @maxResultCount AS INT = 10)
                RETURNS @IDsTable TABLE ( Id INT NOT NULL )
                AS
                BEGIN
                    IF @keywords = ''
                        INSERT INTO @IDsTable
                        SELECT Id FROM Speedy.ItemEntity
                        ORDER BY ItemDescription
                        OFFSET @skipCount ROWS
                        FETCH NEXT @maxResultCount ROWS ONLY
                    ELSE
                        INSERT INTO @IDsTable
                        SELECT Id FROM Speedy.ItemEntity
                        ORDER BY ItemDescription
                        OFFSET @skipCount ROWS
                        FETCH NEXT @maxResultCount ROWS ONLY
                    RETURN
                END;
            ";

            migrationBuilder.Sql(sqlSearchForItemIDsTVF);

            var sql = @"
                CREATE FUNCTION Speedy.ItemSearchTVF (
                    @keywords AS VARCHAR(256) = '',
                    @skipCount AS INT = 0,
                    @maxResultCount AS INT = 10
                )
                RETURNS TABLE
                AS                   
                RETURN 
                    SELECT ieTable.* FROM Speedy.ItemEntity ieTable
                    INNER JOIN Speedy.SearchForItemIDsTVF(@keywords, @skipCount, @maxResultCount) idsTable ON ieTable.id = idsTable.id";

            migrationBuilder.Sql(sql, true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Speedy.ItemSearchTVF", true);
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS Speedy.SearchForItemIDsTVF", true);
        }
    }
}
