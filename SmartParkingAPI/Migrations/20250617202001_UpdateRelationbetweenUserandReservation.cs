using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationbetweenUserandReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReservationRecords_UserId",
                table: "ReservationRecords");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRecords_UserId",
                table: "ReservationRecords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReservationRecords_UserId",
                table: "ReservationRecords");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRecords_UserId",
                table: "ReservationRecords",
                column: "UserId",
                unique: true);
        }
    }
}
