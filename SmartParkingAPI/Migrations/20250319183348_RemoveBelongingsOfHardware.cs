using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParkingAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBelongingsOfHardware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spots_Sensors_SensorId",
                table: "Spots");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "Gates");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Spots_SensorId",
                table: "Spots");

            migrationBuilder.DropColumn(
                name: "SensorId",
                table: "Spots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SensorId",
                table: "Spots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    CameraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GarageId = table.Column<int>(type: "int", nullable: false),
                    CameraLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CameraName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.CameraId);
                    table.ForeignKey(
                        name: "FK_Cameras_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "GarageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GarageId = table.Column<int>(type: "int", nullable: false),
                    GateType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gates_Garages_GarageId",
                        column: x => x.GarageId,
                        principalTable: "Garages",
                        principalColumn: "GarageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    SensorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.SensorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spots_SensorId",
                table: "Spots",
                column: "SensorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_GarageId",
                table: "Cameras",
                column: "GarageId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_GarageId",
                table: "Gates",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spots_Sensors_SensorId",
                table: "Spots",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
