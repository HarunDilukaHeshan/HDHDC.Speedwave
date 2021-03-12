using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Get_rider_mean_distance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
CREATE FUNCTION [Speedy].[getRiderMeanDistance](@addressId INT)
RETURNS @table TABLE(MeanDistance FLOAT)
BEGIN
	DECLARE @cityId INT;
	DECLARE @point01_lat NUMERIC(11, 8), @point01_lng NUMERIC(11, 8);
	DECLARE @count INT SET @count = 0;
	DECLARE @accumulator FLOAT SET @accumulator = 0;

	SET @cityId = NULL;
	SET @point01_lat = NULL;
	SET @point01_lng = NULL;	

	SELECT 
		@point01_lat = [Speedy].[getColumnValue](addrR.Geolocation, ':', 1),
		@point01_lng = [Speedy].[getColumnValue](addrR.Geolocation, ':', 2),
		@cityId = addrR.CityID
	FROM [Speedy].[AddressEntity] addrR WHERE addrR.Id = @addressId

	IF @cityId IS NULL OR @point01_lat IS NULL OR @point01_lng IS NULL
		RETURN;

	DECLARE @rId INT, @geoL VARCHAR(30);

	DECLARE rCursor CURSOR 
	FOR SELECT rR.Id, rR.Geolocation FROM [Speedy].[RiderCoverageEntity] rcR 
	INNER JOIN [Speedy].[RiderEntity] rR on rcR.RiderID = rR.Id
	WHERE rcR.CityID = @cityId;

	OPEN rCursor;

	FETCH NEXT FROM rCursor INTO @rId, @geoL;

	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE @point02_lat NUMERIC(11, 8), @point02_lng NUMERIC(11, 8);
		SET @point02_lat = [Speedy].[getColumnValue](@geoL, ':', 1);
		SET @point02_lng = [Speedy].[getColumnValue](@geoL, ':', 2);

		SET @accumulator += [Speedy].[getDistance02](@point01_lat, @point01_lng, @point02_lat, @point02_lng);
		SET @count += 1;
		FETCH NEXT FROM rCursor INTO @rId, @geoL;
	END;

	CLOSE rCursor;
	DEALLOCATE rCursor;

	If @count > 0
		INSERT INTO @table(MeanDistance) VALUES(@accumulator / @count);

	RETURN;
END;";

			migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var sql = "DROP FUNCTION [Speedy].[getRiderMeanDistance]";
			migrationBuilder.Sql(sql);
        }
    }
}
