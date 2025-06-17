using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParking.API.Migrations
{
    /// <inheritdoc />
    public partial class Add_RevokedOn_and_Some_Other_Things : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ReservationRecords_ReservationRecordId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_UserVerificationCodes_UserId",
                table: "UserVerificationCodes");

            migrationBuilder.RenameColumn(
                name: "ReservationRecordId",
                table: "ReservationRecords",
                newName: "ReservationRecordId");

            migrationBuilder.RenameColumn(
                name: "ReservationRecordId",
                table: "Payments",
                newName: "ReservationRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReservationRecordId",
                table: "Payments",
                newName: "IX_Payments_ReservationRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationCodes_UserId",
                table: "UserVerificationCodes",
                column: "UserId",
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
                name: "IX_UserVerificationCodes_UserId",
                table: "UserVerificationCodes");

            migrationBuilder.RenameColumn(
                name: "ReservationRecordId",
                table: "ReservationRecords",
                newName: "ReservationRecordId");

            migrationBuilder.RenameColumn(
                name: "ReservationRecordId",
                table: "Payments",
                newName: "ReservationRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ReservationRecordId",
                table: "Payments",
                newName: "IX_Payments_ReservationRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationCodes_UserId",
                table: "UserVerificationCodes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ReservationRecords_ReservationRecordId",
                table: "Payments",
                column: "ReservationRecordId",
                principalTable: "ReservationRecords",
                principalColumn: "ReservationRecordId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
