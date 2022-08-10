using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class DbReperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaHall_Show_ShowId",
                table: "CinemaHall");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Show_ShowId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_Reservation_ReservationId",
                table: "Show");

            migrationBuilder.DropIndex(
                name: "IX_Movie_ShowId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_CinemaHall_ShowId",
                table: "CinemaHall");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Show",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_ReservationId",
                table: "Show",
                newName: "IX_Show_MovieId");

            migrationBuilder.RenameColumn(
                name: "ShowId",
                table: "CinemaHall",
                newName: "SeatId");

            migrationBuilder.AddColumn<int>(
                name: "CinemaHallId",
                table: "Show",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Show_CinemaHallId",
                table: "Show",
                column: "CinemaHallId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation",
                column: "ShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Show_ShowId",
                table: "Reservation",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_CinemaHall_CinemaHallId",
                table: "Show",
                column: "CinemaHallId",
                principalTable: "CinemaHall",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Movie_MovieId",
                table: "Show",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Show_ShowId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_CinemaHall_CinemaHallId",
                table: "Show");

            migrationBuilder.DropForeignKey(
                name: "FK_Show_Movie_MovieId",
                table: "Show");

            migrationBuilder.DropIndex(
                name: "IX_Show_CinemaHallId",
                table: "Show");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ShowId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CinemaHallId",
                table: "Show");

            migrationBuilder.DropColumn(
                name: "ShowId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Show",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Show_MovieId",
                table: "Show",
                newName: "IX_Show_ReservationId");

            migrationBuilder.RenameColumn(
                name: "SeatId",
                table: "CinemaHall",
                newName: "ShowId");

            migrationBuilder.AddColumn<int>(
                name: "ShowId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_ShowId",
                table: "Movie",
                column: "ShowId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHall_ShowId",
                table: "CinemaHall",
                column: "ShowId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaHall_Show_ShowId",
                table: "CinemaHall",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Show_ShowId",
                table: "Movie",
                column: "ShowId",
                principalTable: "Show",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Reservation_ReservationId",
                table: "Show",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
