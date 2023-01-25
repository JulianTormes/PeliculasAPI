using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasAPI.Migrations
{
    public partial class ActorsEnglish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Actors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FechaNacimientos",
                table: "Actors",
                newName: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Actors",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Actors",
                newName: "FechaNacimientos");
        }
    }
}
