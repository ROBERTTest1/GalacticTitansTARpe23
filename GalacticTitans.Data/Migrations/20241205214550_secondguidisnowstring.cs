using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticTitans.Data.Migrations
{
    /// <inheritdoc />
    public partial class secondguidisnowstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AstralBodies_SolarSystems_SolarSystemID",
                table: "AstralBodies");

            migrationBuilder.DropIndex(
                name: "IX_AstralBodies_SolarSystemID",
                table: "AstralBodies");

            migrationBuilder.AddColumn<Guid>(
                name: "AstralBodyAtCenterWithID",
                table: "SolarSystems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SolarSystemID",
                table: "AstralBodies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolarSystems_AstralBodyAtCenterWithID",
                table: "SolarSystems",
                column: "AstralBodyAtCenterWithID");

            migrationBuilder.AddForeignKey(
                name: "FK_SolarSystems_AstralBodies_AstralBodyAtCenterWithID",
                table: "SolarSystems",
                column: "AstralBodyAtCenterWithID",
                principalTable: "AstralBodies",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolarSystems_AstralBodies_AstralBodyAtCenterWithID",
                table: "SolarSystems");

            migrationBuilder.DropIndex(
                name: "IX_SolarSystems_AstralBodyAtCenterWithID",
                table: "SolarSystems");

            migrationBuilder.DropColumn(
                name: "AstralBodyAtCenterWithID",
                table: "SolarSystems");

            migrationBuilder.AlterColumn<Guid>(
                name: "SolarSystemID",
                table: "AstralBodies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AstralBodies_SolarSystemID",
                table: "AstralBodies",
                column: "SolarSystemID",
                unique: true,
                filter: "[SolarSystemID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AstralBodies_SolarSystems_SolarSystemID",
                table: "AstralBodies",
                column: "SolarSystemID",
                principalTable: "SolarSystems",
                principalColumn: "ID");
        }
    }
}
