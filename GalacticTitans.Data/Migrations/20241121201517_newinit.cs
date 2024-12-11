using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticTitans.Data.Migrations
{
    /// <inheritdoc />
    public partial class newinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilesToDatabase",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    TitanID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesToDatabase", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Titans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitanHealth = table.Column<int>(type: "int", nullable: false),
                    TitanXP = table.Column<int>(type: "int", nullable: false),
                    TitanXPNextLevel = table.Column<int>(type: "int", nullable: false),
                    TitanLevel = table.Column<int>(type: "int", nullable: false),
                    TitanType = table.Column<int>(type: "int", nullable: false),
                    TitanStatus = table.Column<int>(type: "int", nullable: false),
                    PrimaryAttackPower = table.Column<int>(type: "int", nullable: false),
                    PrimaryAttackName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryAttackPower = table.Column<int>(type: "int", nullable: false),
                    SecondaryAttackName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialAttackPower = table.Column<int>(type: "int", nullable: false),
                    SpecialAttackName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitanWasBorn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitanDied = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AstralBodies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AstralBodyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AstralBodyType = table.Column<int>(type: "int", nullable: false),
                    EnvironmentBoost = table.Column<int>(type: "int", nullable: false),
                    AstralBodyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorSettlements = table.Column<int>(type: "int", nullable: false),
                    TechnicalLevel = table.Column<int>(type: "int", nullable: false),
                    TitanWhoOwnsThisPlanetID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolarSystemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstralBodies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AstralBodies_Titans_TitanWhoOwnsThisPlanetID",
                        column: x => x.TitanWhoOwnsThisPlanetID,
                        principalTable: "Titans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AstralBodies_TitanWhoOwnsThisPlanetID",
                table: "AstralBodies",
                column: "TitanWhoOwnsThisPlanetID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AstralBodies");

            migrationBuilder.DropTable(
                name: "FilesToDatabase");

            migrationBuilder.DropTable(
                name: "Titans");
        }
    }
}
