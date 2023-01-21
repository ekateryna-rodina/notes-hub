using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNote.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUpdatedAtForUserProfileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: false,
                computedColumnSql: "sysutcdatetime()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true,
                oldComputedColumnSql: "sysutcdatetime()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "UserProfiles",
                type: "datetimeoffset",
                nullable: true,
                computedColumnSql: "sysutcdatetime()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldComputedColumnSql: "sysutcdatetime()");
        }
    }
}
