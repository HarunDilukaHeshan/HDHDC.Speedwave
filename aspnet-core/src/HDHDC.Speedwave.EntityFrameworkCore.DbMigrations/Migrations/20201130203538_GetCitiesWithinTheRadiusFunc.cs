using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class GetCitiesWithinTheRadiusFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
					CREATE FUNCTION [Speedy].[getCitiesWithinTheRadius](
						@cityId AS INT,
						@radius AS FLOAT)
					RETURNS @table TABLE(Id INT UNIQUE)
					AS
					BEGIN	
						DECLARE @lat AS NUMERIC(11, 8), @lng AS NUMERIC(11, 8);
						SET @lat = 0;
						SET @lng = 0;

						SELECT 
							@lat = Speedy.getColumnValue(cityR.Geolocation, ':', 1), 
							@lng = Speedy.getColumnValue(cityR.Geolocation, ':', 2) 
						FROM [Speedy].[CityEntity] AS cityR
						WHERE cityR.Id = @cityId

						INSERT INTO @table
						SELECT cityR.Id FROM Speedy.CityEntity AS cityR
						WHERE Speedy.getDistance02(@lat, @lng, Speedy.getColumnValue(cityR.Geolocation, ':', 1), Speedy.getColumnValue(cityR.Geolocation, ':', 2)) < @radius
						AND cityR.IsDeleted = 0
						RETURN;
					END;";
			migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var sql = "DROP FUNCTION [Speedy].[getCitiesWithinTheRadius]";
			migrationBuilder.Sql(sql);
		}
    }
}
