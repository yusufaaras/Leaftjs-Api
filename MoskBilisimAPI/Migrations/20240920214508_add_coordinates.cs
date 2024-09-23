using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoskBilisimAPI.Migrations
{
    /// <inheritdoc />
    public partial class add_coordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MapData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Circles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lat = table.Column<double>(type: "double precision", nullable: false),
                    Lng = table.Column<double>(type: "double precision", nullable: false),
                    Radius = table.Column<double>(type: "double precision", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    MapDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Circles_MapData_MapDataId",
                        column: x => x.MapDataId,
                        principalTable: "MapData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportantPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lat = table.Column<double>(type: "double precision", nullable: false),
                    Lng = table.Column<double>(type: "double precision", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    MapDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportantPoints_MapData_MapDataId",
                        column: x => x.MapDataId,
                        principalTable: "MapData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Markers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lat = table.Column<double>(type: "double precision", nullable: false),
                    Lng = table.Column<double>(type: "double precision", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    MapDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markers_MapData_MapDataId",
                        column: x => x.MapDataId,
                        principalTable: "MapData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Polygons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "text", nullable: false),
                    MapDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polygons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polygons_MapData_MapDataId",
                        column: x => x.MapDataId,
                        principalTable: "MapData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Polylines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "text", nullable: false),
                    MapDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polylines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polylines_MapData_MapDataId",
                        column: x => x.MapDataId,
                        principalTable: "MapData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    PolygonId = table.Column<int>(type: "integer", nullable: true),
                    PolylineId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordinates_Polygons_PolygonId",
                        column: x => x.PolygonId,
                        principalTable: "Polygons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Coordinates_Polylines_PolylineId",
                        column: x => x.PolylineId,
                        principalTable: "Polylines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rectangles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NortheastId = table.Column<int>(type: "integer", nullable: false),
                    SouthwestId = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    MapDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rectangles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rectangles_Coordinates_NortheastId",
                        column: x => x.NortheastId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rectangles_Coordinates_SouthwestId",
                        column: x => x.SouthwestId,
                        principalTable: "Coordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rectangles_MapData_MapDataId",
                        column: x => x.MapDataId,
                        principalTable: "MapData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Circles_MapDataId",
                table: "Circles",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_PolygonId",
                table: "Coordinates",
                column: "PolygonId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_PolylineId",
                table: "Coordinates",
                column: "PolylineId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportantPoints_MapDataId",
                table: "ImportantPoints",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Markers_MapDataId",
                table: "Markers",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Polygons_MapDataId",
                table: "Polygons",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Polylines_MapDataId",
                table: "Polylines",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_MapDataId",
                table: "Rectangles",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_NortheastId",
                table: "Rectangles",
                column: "NortheastId");

            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_SouthwestId",
                table: "Rectangles",
                column: "SouthwestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circles");

            migrationBuilder.DropTable(
                name: "ImportantPoints");

            migrationBuilder.DropTable(
                name: "Markers");

            migrationBuilder.DropTable(
                name: "Rectangles");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "Polygons");

            migrationBuilder.DropTable(
                name: "Polylines");

            migrationBuilder.DropTable(
                name: "MapData");
        }
    }
}
