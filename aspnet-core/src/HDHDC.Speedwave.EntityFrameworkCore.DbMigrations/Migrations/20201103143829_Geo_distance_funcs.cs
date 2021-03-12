using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Geo_distance_funcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var getDistanceSql = "";
            var splitGeoCoordsSql = "";

            getDistanceSql = "" +
                "CREATE FUNCTION [Speedy].[getDistance]( " +
                "@point01_lat FLOAT, " +
                "@point01_lng FLOAT, " +
                "@point02_lat FLOAT, " +
                "@point02_lng FLOAT) " +
            "RETURNS FLOAT " +
            "AS " +
            "BEGIN " +
                "DECLARE @r INT SET @r = 6371 " +
                "DECLARE @lat FLOAT SET @lat = RADIANS(@point02_lat - @point01_lat) " +
                "DECLARE @lng FLOAT SET @lng = RADIANS(@point02_lng - @point01_lng) " +
                "DECLARE @a FLOAT " +
                "SET @a = (SIN(@lat / 2) * SIN(@lat / 2)) + (COS(RADIANS(@point01_lat)) * COS(RADIANS(@point02_lat)) *  SIN(@lng / 2) * SIN(@lng / 2)) " +
                "DECLARE @c FLOAT SET @c = 2 * ATN2(SQRT(@a), SQRT(1 - @a)) " +
                "DECLARE @d FLOAT SET @d = @r * @c " +
                "RETURN @d; " +
            "END; ";

            splitGeoCoordsSql = "" +
                "CREATE FUNCTION [Speedy].[splitGeocoords]( " +
                    "@geocoordsString VARCHAR(50) " +
                ") " +
                "RETURNS @table TABLE(lat VARCHAR(50), lng VARCHAR(50)) " +
                "AS " +
                "BEGIN " +
                    "DECLARE @t TABLE(value VARCHAR(50)) " +
                    "INSERT INTO @t " +
                    "SELECT value FROM string_split(@geocoordsString, ':') " +
                    "DECLARE @lat AS VARCHAR(50), @lng AS VARCHAR(50), @val AS VARCHAR(50) " +
                    "DECLARE cursor_d CURSOR " +
                    "FOR SELECT value FROM @t " +
                    "OPEN cursor_d " +
                    "FETCH NEXT FROM cursor_d INTO @val " +
                    "SET @lat = @val " +
                    "FETCH NEXT FROM cursor_d INTO @val " +
                    "SET @lng = @val " +
                    "CLOSE cursor_d " +
                    "DEALLOCATE cursor_d " +
                    "INSERT INTO @table " +
                    "VALUES (@lat, @lng) " +
                    "RETURN; " +
                "END; ";

            migrationBuilder.Sql(getDistanceSql);
            migrationBuilder.Sql(splitGeoCoordsSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var getDistanceSql = "DROP FUNCTION [SPEEDY].[getDistance]";
            var splitGeoCoordsSql = "DROP FUNCTION [SPEEDY].[splitGeocoords]";

            migrationBuilder.Sql(getDistanceSql);
            migrationBuilder.Sql(splitGeoCoordsSql);
        }
    }
}
