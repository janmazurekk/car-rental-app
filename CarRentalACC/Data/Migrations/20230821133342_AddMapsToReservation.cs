using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalACC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMapsToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EndLatitude",
                table: "Reservations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EndLongitude",
                table: "Reservations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StartLatitude",
                table: "Reservations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "StartLongitude",
                table: "Reservations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndLatitude",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "EndLongitude",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StartLatitude",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StartLongitude",
                table: "Reservations");
        }
    }
}
