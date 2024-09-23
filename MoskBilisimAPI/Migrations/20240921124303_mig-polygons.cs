using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoskBilisimAPI.Migrations
{
    /// <inheritdoc />
    public partial class migpolygons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Polygons");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Polygons");

            migrationBuilder.AddColumn<int>(
                name: "PolygonId",
                table: "Coordinates",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_PolygonId",
                table: "Coordinates",
                column: "PolygonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Polygons_PolygonId",
                table: "Coordinates",
                column: "PolygonId",
                principalTable: "Polygons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Polygons_PolygonId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_PolygonId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "PolygonId",
                table: "Coordinates");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Polygons",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Polygons",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
