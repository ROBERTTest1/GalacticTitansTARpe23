using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalacticTitans.Data.Migrations
{
    /// <inheritdoc />
    public partial class astralbodyidsinsolarsystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AstralBodyIDs",
                table: "SolarSystems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AstralBodyIDs",
                table: "SolarSystems");
        }
    }
}
