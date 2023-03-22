using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistanFerghanaLC.DataAccess.Migrations
{
    public partial class UpdateTeachersEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Teachers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Teachers");
        }
    }
}
