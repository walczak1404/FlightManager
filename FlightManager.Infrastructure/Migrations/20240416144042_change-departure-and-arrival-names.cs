using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changedepartureandarrivalnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureAirport",
                table: "Flights",
                newName: "DepartureCity");

            migrationBuilder.RenameColumn(
                name: "ArrivalAirport",
                table: "Flights",
                newName: "ArrivalCity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureCity",
                table: "Flights",
                newName: "DepartureAirport");

            migrationBuilder.RenameColumn(
                name: "ArrivalCity",
                table: "Flights",
                newName: "ArrivalAirport");
        }
    }
}
