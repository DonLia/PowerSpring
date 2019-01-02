using Microsoft.EntityFrameworkCore.Migrations;

namespace PowerSpring.Migrations
{
    public partial class NewsChangedMirgration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "News",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "News",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "News");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "News");
        }
    }
}
