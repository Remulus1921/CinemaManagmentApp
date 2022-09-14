using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class MovieTitleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Show_Movie_MovieId",
                table: "Show");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Show",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Show_Movie_MovieId",
                table: "Show");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Show",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Show_Movie_MovieId",
                table: "Show",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");
        }
    }
}
