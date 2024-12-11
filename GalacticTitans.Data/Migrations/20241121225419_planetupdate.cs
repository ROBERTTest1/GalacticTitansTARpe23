using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticTitans.Data.Migrations
{
    /// <inheritdoc />
    public partial class planetupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AstralBodies_Titans_TitanWhoOwnsThisPlanetID",
                table: "AstralBodies");

            migrationBuilder.AddColumn<Guid>(
                name: "AstralBodyID",
                table: "FilesToDatabase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TitanWhoOwnsThisPlanetID",
                table: "AstralBodies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SolarSystemID",
                table: "AstralBodies",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_AstralBodies_Titans_TitanWhoOwnsThisPlanetID",
                table: "AstralBodies",
                column: "TitanWhoOwnsThisPlanetID",
                principalTable: "Titans",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AstralBodies_Titans_TitanWhoOwnsThisPlanetID",
                table: "AstralBodies");

            migrationBuilder.DropColumn(
                name: "AstralBodyID",
                table: "FilesToDatabase");

            migrationBuilder.AlterColumn<Guid>(
                name: "TitanWhoOwnsThisPlanetID",
                table: "AstralBodies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SolarSystemID",
                table: "AstralBodies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AstralBodies_Titans_TitanWhoOwnsThisPlanetID",
                table: "AstralBodies",
                column: "TitanWhoOwnsThisPlanetID",
                principalTable: "Titans",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
