using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class SeatsToShowAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_CinemaHall_CinemaHallId",
                table: "Seat");

            migrationBuilder.RenameColumn(
                name: "CinemaHallId",
                table: "Seat",
                newName: "ShowId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_CinemaHallId",
                table: "Seat",
                newName: "IX_Seat_ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_Show_ShowId",
                table: "Seat",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_Show_ShowId",
                table: "Seat");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "Seat",
                newName: "CinemaHallId");

            migrationBuilder.RenameIndex(
                name: "IX_Seat_ShowId",
                table: "Seat",
                newName: "IX_Seat_CinemaHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_CinemaHall_CinemaHallId",
                table: "Seat",
                column: "CinemaHallId",
                principalTable: "CinemaHall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
