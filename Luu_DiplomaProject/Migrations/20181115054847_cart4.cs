using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class cart4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblCart_CartId",
                table: "TblHamper");

            migrationBuilder.DropIndex(
                name: "IX_TblHamper_CartId",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "TblHamper");

            migrationBuilder.AddColumn<int>(
                name: "HamperId",
                table: "TblCart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblCart_HamperId",
                table: "TblCart",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCart_TblHamper_HamperId",
                table: "TblCart",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCart_TblHamper_HamperId",
                table: "TblCart");

            migrationBuilder.DropIndex(
                name: "IX_TblCart_HamperId",
                table: "TblCart");

            migrationBuilder.DropColumn(
                name: "HamperId",
                table: "TblCart");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "TblHamper",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_CartId",
                table: "TblHamper",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblCart_CartId",
                table: "TblHamper",
                column: "CartId",
                principalTable: "TblCart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
