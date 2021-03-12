using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class addedRiderSearchFuncs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = "" +
                "CREATE FUNCTION Speedy.searchForRider( " +
                    "@keywordsStr VARCHAR(MAX), " +
                    "@districtId VARCHAR(MAX)," +
                    "@skipCount INT = 0," +
                    "@maxResultCount INT = 10) " +
                "RETURNS @IDsTable TABLE ( Id INT NOT NULL ) " +
                "AS " +
                "BEGIN " +
                "IF @keywordsStr = '' " +
                "BEGIN " +
                "INSERT INTO @IDsTable " +
                "SELECT riderE.Id FROM Speedy.RiderEntity AS riderE " +
                "INNER JOIN CityEntity AS cityE ON riderE.CityID = cityE.Id " +
                "WHERE cityE.DistrictID = @districtId " +
                "ORDER BY riderE.Id " +
                "OFFSET @skipCount ROWS " +
                "FETCH NEXT @maxResultCount ROWS ONLY " +
                "RETURN; " +
                "END; " +
                "" +
                "DECLARE @keyword AS VARCHAR(100) " +
                "DECLARE keywords_cursor CURSOR " +
                "FOR SELECT value FROM string_split(@keywordsStr, ',') " +
                "OPEN keywords_cursor " +
                "FETCH NEXT FROM keywords_cursor INTO @keyword " +
                "WHILE @@FETCH_STATUS = 0 " +
                    "BEGIN " +
                        "INSERT INTO @IDsTable " +
                        "SELECT riderE.Id FROM Speedy.RiderEntity AS riderE " +
                        "INNER JOIN dbo.AbpUsers AS userE ON riderE.UserID = userE.Id " +
                        "INNER JOIN Speedy.CityEntity AS cityE ON riderE.CityID = cityE.Id " +
                        "WHERE " +
                            "cityE.DistrictID LIKE @districtId AND " +
                            "(userE.UserName LIKE @keyword OR " +
                            "userE.Name LIKE '%' + @keyword + '%' OR " +
                            "userE.Surname LIKE '%' + @keyword + '%' OR " +
                            "userE.PhoneNumber LIKE @keyword OR " +
                            "userE.Email LIKE @keyword) " +
                            "ORDER BY userE.UserName " +
                            "OFFSET @skipCount ROWS " +
                            "FETCH NEXT @maxResultCount ROWS ONLY " +
                            "" +
                        "FETCH NEXT FROM keywords_cursor INTO @keyword " +
                     "END " +
                 "CLOSE keywords_cursor; " +
                 "DEALLOCATE keywords_cursor; " +
                 "RETURN; " +
                 "END; ";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DROP FUNCTION Speedy.searchForRider";
            migrationBuilder.Sql(sql);
        }
    }
}
