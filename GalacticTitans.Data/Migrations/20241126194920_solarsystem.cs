using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticTitans.Data.Migrations
{
    /// <inheritdoc />
    public partial class solarsystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolarSystems",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AstralBodyAtCenter = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolarSystemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolarSystemLore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarSystems", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AstralBodies_SolarSystemID",
                table: "AstralBodies",
                column: "SolarSystemID",
                unique: false, //manual change to false
                filter: "[SolarSystemID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AstralBodies_SolarSystems_SolarSystemID",
                table: "AstralBodies",
                column: "SolarSystemID",
                principalTable: "SolarSystems",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AstralBodies_SolarSystems_SolarSystemID",
                table: "AstralBodies");

            migrationBuilder.DropTable(
                name: "SolarSystems");

            migrationBuilder.DropIndex(
                name: "IX_AstralBodies_SolarSystemID",
                table: "AstralBodies");
        }
    }
}
