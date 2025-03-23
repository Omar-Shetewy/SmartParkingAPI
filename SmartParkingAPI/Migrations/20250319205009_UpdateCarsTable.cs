using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParkingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpotId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_SpotId",
                table: "Cars",
                column: "SpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Spots_SpotId",
                table: "Cars",
                column: "SpotId",
                principalTable: "Spots",
                principalColumn: "SpotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Spots_SpotId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_SpotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SpotId",
                table: "Cars");
        }
    }
}
