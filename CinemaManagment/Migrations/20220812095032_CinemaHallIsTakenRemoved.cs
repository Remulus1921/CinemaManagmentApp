using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class CinemaHallIsTakenRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "CinemaHall");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation",
                column: "ShowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation");

            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "CinemaHall",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation",
                column: "ShowId",
                unique: true);
        }
    }
}
