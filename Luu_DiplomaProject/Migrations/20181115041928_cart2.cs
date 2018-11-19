using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class cart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HamperId",
                table: "Cart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HamperId",
                table: "Cart",
                nullable: false,
                defaultValue: 0);
        }
    }
}
