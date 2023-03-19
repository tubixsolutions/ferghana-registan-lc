using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistanFerghanaLC.DataAccess.Migrations
{
    public partial class forextralesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LessonTopic",
                table: "ExtraLessons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonTopic",
                table: "ExtraLessons");
        }
    }
}
