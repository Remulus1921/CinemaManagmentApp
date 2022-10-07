using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class SeatNrAddedToReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatNr",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatNr",
                table: "Reservation");
        }
    }
}
