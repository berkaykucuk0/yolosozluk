using Microsoft.EntityFrameworkCore.Migrations;

namespace YoloSozluk.Infrastructure.Persistence.Migrations
{
    public partial class updated_username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "dbo",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "dbo",
                table: "user");
        }
    }
}
