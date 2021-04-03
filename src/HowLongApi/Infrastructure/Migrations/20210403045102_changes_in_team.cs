using Microsoft.EntityFrameworkCore.Migrations;

namespace HowLongApi.Migrations
{
    public partial class changes_in_team : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Team");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Team",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
