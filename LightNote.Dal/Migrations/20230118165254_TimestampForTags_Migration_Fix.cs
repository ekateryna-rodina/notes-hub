using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNote.Dal.Migrations
{
    /// <inheritdoc />
    public partial class TimestampForTagsMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tags",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "getutcdate()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComputedColumnSql: "getutcdate()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Tags",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Tags",
                type: "datetimeoffset",
                nullable: false,
                computedColumnSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComputedColumnSql: "getutcdate()");
        }
    }
}
