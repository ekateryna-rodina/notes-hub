using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNote.Dal.Migrations
{
    /// <inheritdoc />
    public partial class LocationChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Location_BasicUserInfo_LocationId",
                table: "UserProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Location_LocationId",
                table: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_BasicUserInfo_LocationId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_LocationId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "BasicUserInfo_LocationId",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "BasicUserInfo_City",
                table: "UserProfiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BasicUserInfo_Country",
                table: "UserProfiles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicUserInfo_City",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "BasicUserInfo_Country",
                table: "UserProfiles");

            migrationBuilder.AddColumn<Guid>(
                name: "BasicUserInfo_LocationId",
                table: "UserProfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "UserProfiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_BasicUserInfo_LocationId",
                table: "UserProfiles",
                column: "BasicUserInfo_LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_LocationId",
                table: "UserProfiles",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Location_BasicUserInfo_LocationId",
                table: "UserProfiles",
                column: "BasicUserInfo_LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Location_LocationId",
                table: "UserProfiles",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");
        }
    }
}
