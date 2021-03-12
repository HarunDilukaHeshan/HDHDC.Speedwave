using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Compatible_delivery_schedules_func : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			var sqlProc = @"
				CREATE FUNCTION [Speedy].[getCompatibleDsSchedules](
					@addressId INT,
					@itemIdsStr VARCHAR(MAX),
					@radius FLOAT,
					@refDateTime DATETIME)
				RETURNS @dsTable TABLE(Id INT UNIQUE)
				AS
				BEGIN
					--SET NOCOUNT ON;
				
					-- STEP 01
					DECLARE @geoLStr AS VARCHAR(30) SET @geoLStr = '';
					DECLARE @itemIdsTable AS TABLE(Id INT UNIQUE);
					DECLARE @itemIdsCount AS INT SET @itemIdsCount = 0;

					SELECT @geoLStr = addrR.Geolocation FROM [Speedy].[AddressEntity] AS addrR WHERE addrR.IsDeleted = 0 AND addrR.Id = @addressId
				
					IF @geoLStr = '' 
						RETURN;
						--THROW 55555, 'Invalid addressId', 16;

					INSERT INTO @itemIdsTable
					SELECT DISTINCT itemR.Id FROM [Speedy].[ItemEntity] AS itemR 
					CROSS APPLY (SELECT DISTINCT itemIdR.value FROM  STRING_SPLIT(@itemIdsStr, ',') AS itemIdR WHERE itemIdR.value = itemR.Id) AS sItemR
					WHERE itemR.IsDeleted = 0;

					SELECT @itemIdsCount = COUNT(DISTINCT iidsR.Id) FROM @itemIdsTable AS iidsR

					IF @itemIdsCount != (SELECT COUNT(DISTINCT iidsStrR.value) FROM STRING_SPLIT(@itemIdsStr, ',') AS iidsStrR)
						RETURN;
						--THROW 55556, 'Invalid itemId found', 16;

					-- STEP 02

					DECLARE @lat AS NUMERIC(11, 8) SET @lat = dbo.getColumnValue(@geoLStr, ':', 1);
					DECLARE @lng AS NUMERIC(11, 8) SET @lng = dbo.getColumnValue(@geoLStr, ':', 2);

					DECLARE @sbTable TABLE(Id INT UNIQUE)
					INSERT INTO @sbTable SELECT DISTINCT ssbR.Id FROM [Speedy].getStoreBranches02(@lat, @lng, @radius) AS ssbR
					INNER JOIN [Speedy].[StoreBranchEntity] AS sbR ON ssbR.Id = sbR.Id WHERE sbR.IsDeleted = 0

					DECLARE @selectedSbTable TABLE(Id INT UNIQUE)
					INSERT INTO @selectedSbTable SELECT DISTINCT sbR.Id FROM @sbTable AS sbR
					INNER JOIN [Speedy].[ItemStoreBranchEntity] AS isbR ON sbR.Id = isbR.StoreBranchID 
					INNER JOIN @itemIdsTable AS iidsTR ON isbR.ItemID = iidsTR.Id

					IF (@itemIdsCount != (
						SELECT COUNT(DISTINCT iidsR.Id) FROM @itemIdsTable AS iidsR 
						INNER JOIN [Speedy].[ItemStoreBranchEntity] AS isbR ON iidsR.Id = isbR.ItemID 
						INNER JOIN @selectedSbTable AS ssbR ON isbR.StoreBranchID = ssbR.Id))
							RETURN;

					-- STEP 03

					DECLARE dsCursor CURSOR FOR SELECT dsR.Id, dsR.TimePeriod FROM [Speedy].[DeliveryScheduleEntity] AS dsR WHERE dsR.IsDeleted = 0
					DECLARE @dsId AS INT, @dsTimePeriod AS BIGINT;

					OPEN dsCursor;
					FETCH NEXT FROM dsCursor INTO @dsId, @dsTimePeriod;

					DECLARE @selectedDsTable TABLE(Id INT);

					-- Delivery schedule loop
					WHILE @@FETCH_STATUS = 0
					BEGIN
						DECLARE sbCursor Cursor FOR SELECT ssbTR.Id FROM @selectedSbTable AS ssbTR;
						DECLARE @sbId AS INT SET @sbId = 0;

						DECLARE @dsInMs AS BIGINT SET @dsInMs = @dsTimePeriod;
						DECLARE @dsInDays AS INT SET @dsInDays = @dsTimePeriod / (60 * 60 * 24 * 1000);	

						DECLARE @tempSbTable AS TABLE(Id INT UNIQUE);
						DELETE FROM @tempSbTable;
						INSERT INTO @tempSbTable SELECT Id FROM @selectedSbTable

						OPEN sbCursor;
						FETCH NEXT FROM sbCursor INTO @sbId;

						WHILE @@FETCH_STATUS = 0
						BEGIN
							DECLARE @sbClosingDate AS DATE;
							DECLARE @sbOpeningTime AS TIME, @sbClosingTime AS TIME;
							DECLARE @branchClosed AS BIT SET @branchClosed = 0;

							SET @sbClosingDate = NULL;
							SET @sbOpeningTime = NULL;
							SET @sbClosingTime = NULL;

							SELECT @sbClosingDate = scdR.ClosingDate FROM [Speedy].[StoreClosingDateEntity] AS scdR
							WHERE scdR.IsDeleted = 0 AND DATEDIFF(day, @refDateTime, scdR.ClosingDate) = @dsInDays AND scdR.StoreBranchID = @sbId
		
							SELECT @sbOpeningTime = sodR.OpeningTime, @sbClosingTime = sodR.ClosingTime FROM [Speedy].[StoreOpenDayEntity] AS sodR 
							WHERE sodR.DayOfWeek = DATEPART(DW, DATEADD(DAY, @dsInDays, @refDateTime)) - 1 AND sodR.StoreBranchID = @sbId

							IF @dsInDays = 0
							BEGIN
								IF @sbOpeningTime IS NOT NULL AND @sbClosingTime IS NOT NULL AND @sbClosingDate IS NULL
								BEGIN
									DECLARE @oH FLOAT, @cH FLOAT, @rH FLOAT;
									SET @oH = DATEPART(HH, @sbOpeningTime) + (DATEPART(MI, @sbOpeningTime) / 60);
									SET @cH = DATEPART(HH, @sbClosingTime) + (DATEPART(MI, @sbClosingTime) / 60);
									SET @rH = DATEPART(HH, @refDateTime) + (DATEPART(MI, @refDateTime) / 60);

									IF @oH > @rH 
										SET @branchClosed = 1
									ELSE IF (@rH + (@dsInMs / (60 * 60 * 1000))) > @cH
										SET @branchClosed = 1
								END;
								ELSE
									SET @branchClosed = 1				
							END;
							ELSE
								IF @sbOpeningTime IS NULL OR @sbClosingTime IS NULL OR @sbClosingDate IS NOT NULL
									SET @branchClosed = 1;

							IF @branchClosed = 1
							BEGIN
								DECLARE @curSbItemsTable TABLE(Id INT UNIQUE);
								DECLARE @oSbItemsTable TABLE(Id INT UNIQUE);
								DECLARE @count AS INT SET @count = 0;

								DELETE FROM @curSbItemsTable;
								DELETE FROM @oSbItemsTable;

								INSERT INTO @curSbItemsTable SELECT iidsR.Id FROM @itemIdsTable AS iidsR
								INNER JOIN [Speedy].[ItemStoreBranchEntity] AS isbR ON iidsR.Id = isbR.ItemID 
								WHERE isbR.StoreBranchID = @sbId

								DELETE FROM @tempSbTable WHERE Id = @sbId

								INSERT INTO @oSbItemsTable 
								SELECT iidsR.Id FROM @itemIdsTable AS iidsR
								INNER JOIN [Speedy].[ItemStoreBranchEntity] AS isbR ON iidsR.Id = isbR.ItemID 
								INNER JOIN @tempSbTable AS tsbR ON isbR.StoreBranchID = tsbR.Id

								SELECT @count = COUNT(DISTINCT iidsR.Id) FROM @itemIdsTable AS iidsR
								INNER JOIN @curSbItemsTable AS curiR ON iidsR.Id = curiR.Id
								INNER JOIN @oSbItemsTable AS oiR ON curiR.Id = oiR.Id

								IF @count = (SELECT COUNT(*) FROM @curSbItemsTable)
									INSERT INTO @selectedDsTable(Id) VALUES(@dsId);
								ELSE
								BEGIN
									DELETE FROM @selectedDsTable WHERE Id = @dsId;
									BREAK;
								END;
							END;
							ELSE
								INSERT INTO @selectedDsTable(Id) VALUES(@dsId);
							FETCH NEXT FROM sbCursor INTO @sbId;
						END;

						-- Store branch
						CLOSE sbCursor;
						DEALLOCATE sbCursor;

						FETCH NEXT FROM dsCursor INTO @dsId, @dsTimePeriod;
					END;

					-- Delivery schedule
					CLOSE dsCursor;
					DEALLOCATE dsCursor;

					INSERT INTO @dsTable
					SELECT DISTINCT Id FROM @selectedDsTable;

					RETURN;					
				END;";

			migrationBuilder.Sql(sqlProc);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			var sqlProc = "DROP PROCEDURE [Speedy].[getCompatibleDsSchedules]";

			migrationBuilder.Sql(sqlProc);
		}
	}
}
