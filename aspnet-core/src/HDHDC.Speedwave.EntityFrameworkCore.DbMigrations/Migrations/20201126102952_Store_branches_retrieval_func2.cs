using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Store_branches_retrieval_func2 : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var sql = @"
				CREATE FUNCTION [Speedy].[getStoreBranches02]( 
					@point01_lat NUMERIC(11, 8), 
					@point01_lng NUMERIC(11, 8), 
					@radius FLOAT ) 
				RETURNS @table TABLE(Id INT) 
				AS 
				BEGIN 
					DECLARE @id INT 
					DECLARE @geolocation VARCHAR(50) 
					DECLARE branches_cursor CURSOR 
					FOR SELECT Id, Geolocation FROM Speedy.StoreBranchEntity 
					OPEN branches_cursor 
					FETCH NEXT FROM branches_cursor INTO @id, @geolocation 
					WHILE @@FETCH_STATUS = 0 
					BEGIN 						
						DECLARE @lat AS NUMERIC(11, 8) SET @lat = [dbo].[getColumnValue](@geolocation, ':', 1);
						DECLARE @lng AS NUMERIC(11, 8) SET @lng = [dbo].[getColumnValue](@geolocation, ':', 2);
						DECLARE @d NUMERIC(11, 8) SET @d = Speedy.getDistance02(@point01_lat, @point01_lng, @lat, @lng) 
						IF @d < @radius INSERT INTO @table VALUES(@id) 
						FETCH NEXT FROM branches_cursor INTO @id, @geolocation 
					END; 
					CLOSE branches_cursor; 
					DEALLOCATE branches_cursor; 
					RETURN; 
				END;";

			migrationBuilder.Sql(sql);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			var sql = "DROP FUNCTION [Speedy].[getStoreBranches02]";
			migrationBuilder.Sql(sql);
		}
	}
}
