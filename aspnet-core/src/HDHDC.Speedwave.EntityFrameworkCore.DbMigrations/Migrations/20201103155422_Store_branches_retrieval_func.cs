using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Store_branches_retrieval_func : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var getStoreBranchesSql = "" +
                "CREATE FUNCTION [Speedy].[getStoreBranches]( " +
                    "@point01_lat FLOAT, " +
                    "@point01_lng FLOAT, " +
                    "@radius FLOAT " +
                ") " +
                "RETURNS @table TABLE(Id INT) " +
                "AS " +
                "BEGIN " +
                    "DECLARE @id INT " +
                    "DECLARE @geolocation VARCHAR(50) " +
                    "DECLARE branches_cursor CURSOR " +
                    "FOR SELECT Id, Geolocation FROM Speedy.StoreBranchEntity " +
                    "OPEN branches_cursor " +
                    "FETCH NEXT FROM branches_cursor INTO @id, @geolocation " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "BEGIN " +
                        "DECLARE @lat VARCHAR(50), @lng VARCHAR(50) " +
                        "DECLARE @geoT TABLE(lat VARCHAR(50), lng VARCHAR(50)) " +
                        "INSERT INTO @geoT SELECT * FROM Speedy.splitGeocoords(@geolocation) " +
                        "SELECT TOP 1 @lat = lat, @lng = lng FROM @geoT " +
                        "DELETE FROM @geoT " +
                        "DECLARE @d FLOAT SET @d = Speedy.getDistance(@point01_lat, @point01_lng, @lat, @lng) " +
                        "IF @d < @radius " +
                            "INSERT INTO @table VALUES(@id) " +
                        "FETCH NEXT FROM branches_cursor INTO @id, @geolocation " +
                    "END; " +
                    "CLOSE branches_cursor; " +
                    "DEALLOCATE branches_cursor; " +
                    "RETURN; " +
                "END; ";

            migrationBuilder.Sql(getStoreBranchesSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var getStoreBranchesSql = "DROP FUNCTION [Speedy].[getStoreBranches]";
            migrationBuilder.Sql(getStoreBranchesSql);
        }
    }
}
