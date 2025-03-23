using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParkingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservationRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GarageId",
                table: "ReservationRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRecords_GarageId",
                table: "ReservationRecords",
                column: "GarageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRecords_Garages_GarageId",
                table: "ReservationRecords",
                column: "GarageId",
                principalTable: "Garages",
                principalColumn: "GarageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRecords_Garages_GarageId",
                table: "ReservationRecords");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRecords_GarageId",
                table: "ReservationRecords");

            migrationBuilder.DropColumn(
                name: "GarageId",
                table: "ReservationRecords");
        }
    }
}
