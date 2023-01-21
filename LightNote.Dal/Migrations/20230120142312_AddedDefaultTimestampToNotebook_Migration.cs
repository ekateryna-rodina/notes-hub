using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNote.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultTimestampToNotebookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Notebooks",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "sysutcdatetime()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Notebooks",
                type: "datetimeoffset",
                nullable: true,
                computedColumnSql: "sysutcdatetime()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Notebooks",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true,
                oldComputedColumnSql: "sysutcdatetime()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Notebooks",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "sysutcdatetime()");
        }
    }
}
