using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNote.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTimestampsForUserProfileMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Notebooks",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "sysutcdatetime()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Notebooks",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "getutcdate()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true,
                oldComputedColumnSql: "sysutcdatetime()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Notebooks",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "sysutcdatetime()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Notebooks",
                type: "datetimeoffset",
                nullable: true,
                computedColumnSql: "sysutcdatetime()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComputedColumnSql: "getutcdate()");
        }
    }
}
