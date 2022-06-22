using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carmarket.Application.Identity.IS4.Migrations
{
    public partial class AddUserSubjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "AspNetUsers");
        }
    }
}
