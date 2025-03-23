using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartParkingAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGaragesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Garages");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Garages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Garages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Garages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Garages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
