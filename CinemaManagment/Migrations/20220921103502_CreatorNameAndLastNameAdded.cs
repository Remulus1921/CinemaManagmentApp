using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaManagment.Migrations
{
    public partial class CreatorNameAndLastNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorFirstName",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorLastName",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorFirstName",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CreatorLastName",
                table: "Reservation");
        }
    }
}
