using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class CinemaHallChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_CinemaHall_CinemaHallId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Seat_CinemaHallId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "CinemaHallId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "CinemaHall");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaHallId",
                table: "Seat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatId",
                table: "CinemaHall",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seat_CinemaHallId",
                table: "Seat",
                column: "CinemaHallId");

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
