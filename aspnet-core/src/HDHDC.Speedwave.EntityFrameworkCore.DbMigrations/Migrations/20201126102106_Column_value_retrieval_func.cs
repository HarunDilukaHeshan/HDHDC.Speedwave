using Microsoft.EntityFrameworkCore.Migrations;

namespace HDHDC.Speedwave.Migrations
{
    public partial class Column_value_retrieval_func : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
				CREATE FUNCTION [Speedy].[getColumnValue](
					@String VARCHAR(MAX),
					@Delimiter CHAR(1),
					@Column INT = 1
				)
				RETURNS VARCHAR(MAX)
				AS
				BEGIN
				DECLARE @idx INT
				DECLARE @slice VARCHAR(MAX)
				SELECT @idx = 1
					IF LEN(@String) < 1 OR @String IS NULL
						RETURN NULL
				DECLARE @ColCnt INT SET @ColCnt = 1
				WHILE (@idx != 0)
				BEGIN
					SET @idx = CHARINDEX(@Delimiter, @String)
					IF @idx != 0
					BEGIN
						IF (@ColCnt = @Column)
							RETURN LEFT(@String, @idx - 1)
						SET @ColCnt = @ColCnt + 1
					END
					SET @String = RIGHT(@String, LEN(@String) - @idx)
					IF LEN(@String) = 0 BREAK
				END
				RETURN @String
				END";

			migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			var sql = "DROP FUNCTION ";
			migrationBuilder.Sql(sql);
		}
    }
}
