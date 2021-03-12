using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class search_items_within_the_radius_func : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
CREATE FUNCTION [Speedy].[searchItemsWithinRadius](
	@cityId INT,
	@radius FLOAT,
	@keywords VARCHAR(1024) = '',
	@skipCount INT = 0,
	@maxResultCount INT = 10)
RETURNS @table TABLE(Id INT UNIQUE)
AS
BEGIN
	DECLARE @sbTable TABLE(Id INT UNIQUE);
	DECLARE @cityGeoL VARCHAR(30) SET @cityGeoL = NULL;

	SELECT @cityGeoL = cR.Geolocation FROM [Speedy].[CityEntity] cR WHERE cR.IsDeleted = 0 AND cR.Id = @cityId;

	IF @cityGeoL IS NULL
		RETURN;

	DECLARE @lat NUMERIC(11, 8), @lng NUMERIC(11, 8);

	SET @lat = NULL; SET @lng = NULL;

	SET @lat = [Speedy].[getColumnValue](@cityGeoL, ':', 1);
	SET @lng = [Speedy].[getColumnValue](@cityGeoL, ':', 2);

	IF @lat IS NULL OR @lng IS NULL
		RETURN;

	INSERT INTO @sbTable
	SELECT Id FROM [Speedy].[getStoreBranches02](@lat, @lng, @radius);

	INSERT INTO @table
	SELECT DISTINCT iR.Id FROM [Speedy].[ItemEntity] AS iR
	INNER JOIN [Speedy].[ItemStoreBranchEntity] AS isbR ON iR.Id = isbR.ItemID
	INNER JOIN [Speedy].[searchForItems](@keywords, @skipCount, @maxResultCount) AS sfiR ON isbR.ItemID = sfiR.Id
	INNER JOIN @sbTable AS sbtR ON isbR.StoreBranchID = sbtR.Id;
	RETURN;
END;";
			migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var sql = "DROP FUNCTION [Speedy].[searchItemsWithinRadius]";
			migrationBuilder.Sql(sql);
		}
    }
}
