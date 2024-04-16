using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class departuredateUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Flights",
                newName: "DepartureDateUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureDateUTC",
                table: "Flights",
                newName: "DepartureDate");
        }
    }
}
