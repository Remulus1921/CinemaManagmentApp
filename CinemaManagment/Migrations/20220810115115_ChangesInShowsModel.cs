using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class ChangesInShowsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation",
                column: "ShowId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation",
                column: "ShowId");
        }
    }
}
