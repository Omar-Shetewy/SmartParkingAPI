using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationPaymentsAndReservationsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRecords_Payments_PaymentId",
                table: "ReservationRecords");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRecords_PaymentId",
                table: "ReservationRecords");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "ReservationRecords");

            migrationBuilder.AddColumn<int>(
                name: "ReservationRecordId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ReservationRecordId",
                table: "Payments",
                column: "ReservationRecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ReservationRecords_ReservationRecordId",
                table: "Payments",
                column: "ReservationRecordId",
                principalTable: "ReservationRecords",
                principalColumn: "ReservationRecordId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ReservationRecords_ReservationRecordId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ReservationRecordId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ReservationRecordId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "ReservationRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRecords_PaymentId",
                table: "ReservationRecords",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRecords_Payments_PaymentId",
                table: "ReservationRecords",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
