using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticTitans.Data.Migrations
{
    /// <inheritdoc />
    public partial class playerprofiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Titans",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnershipCreatedAt",
                table: "Titans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnershipID",
                table: "Titans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnershipUpdatedAt",
                table: "Titans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerProfileID",
                table: "Titans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayerProfiles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GalacticCredits = table.Column<int>(type: "int", nullable: false),
                    ScrapResource = table.Column<int>(type: "int", nullable: false),
                    Victories = table.Column<int>(type: "int", nullable: false),
                    MySolarSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStatus = table.Column<int>(type: "int", nullable: false),
                    ProfileType = table.Column<bool>(type: "bit", nullable: false),
                    ProfileCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileAttributedToAnAccountUserAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileStatusLastChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerProfiles", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Titans_PlayerProfileID",
                table: "Titans",
                column: "PlayerProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Titans_PlayerProfiles_PlayerProfileID",
                table: "Titans",
                column: "PlayerProfileID",
                principalTable: "PlayerProfiles",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titans_PlayerProfiles_PlayerProfileID",
                table: "Titans");

            migrationBuilder.DropTable(
                name: "PlayerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Titans_PlayerProfileID",
                table: "Titans");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Titans");

            migrationBuilder.DropColumn(
                name: "OwnershipCreatedAt",
                table: "Titans");

            migrationBuilder.DropColumn(
                name: "OwnershipID",
                table: "Titans");

            migrationBuilder.DropColumn(
                name: "OwnershipUpdatedAt",
                table: "Titans");

            migrationBuilder.DropColumn(
                name: "PlayerProfileID",
                table: "Titans");
        }
    }
}
