using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Geo_distance_func2 : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var sql = @"
				CREATE FUNCTION [Speedy].[getDistance02]( 
					@point01_lat NUMERIC(11, 8), 
					@point01_lng NUMERIC(11, 8), 
					@point02_lat NUMERIC(11, 8),
					@point02_lng NUMERIC(11, 8)) 
				RETURNS NUMERIC(11, 8)
				AS 
				BEGIN 
					DECLARE @r INT SET @r = 6371 
					DECLARE @lat NUMERIC(11, 8) SET @lat = RADIANS(@point02_lat - @point01_lat) 
					DECLARE @lng NUMERIC(11, 8) SET @lng = RADIANS(@point02_lng - @point01_lng) 
					DECLARE @a NUMERIC(11, 8) SET @a = (SIN(@lat / 2) * SIN(@lat / 2)) + (COS(RADIANS(@point01_lat)) * COS(RADIANS(@point02_lat)) *  SIN(@lng / 2) * SIN(@lng / 2)) 
					DECLARE @c NUMERIC(11, 8) SET @c = 2 * ATN2(SQRT(@a), SQRT(1 - @a)) 
					DECLARE @d NUMERIC(11, 8) SET @d = @r * @c 
					RETURN @d; 
				END;";

			migrationBuilder.Sql(sql);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			var sql = "DROP FUNCTION [Speedy].[getDistance02]";
			migrationBuilder.Sql(sql);
		}
	}
}
