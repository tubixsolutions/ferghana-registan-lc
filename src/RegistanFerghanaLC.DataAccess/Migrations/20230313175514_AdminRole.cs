using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistanFerghanaLC.DataAccess.Migrations
{
    public partial class AdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminRole",
                table: "Admins",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRole",
                table: "Admins");
        }
    }
}
