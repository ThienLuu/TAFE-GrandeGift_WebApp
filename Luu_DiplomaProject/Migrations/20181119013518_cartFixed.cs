using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class cartFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblOrderDetail_OrderDetailId",
                table: "TblHamper");

            migrationBuilder.DropIndex(
                name: "IX_TblHamper_OrderDetailId",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "OrderDetails",
                table: "TblHamper");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderDetail_HamperId",
                table: "TblOrderDetail",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderDetail_TblHamper_HamperId",
                table: "TblOrderDetail",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderDetail_TblHamper_HamperId",
                table: "TblOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderDetail_HamperId",
                table: "TblOrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetails",
                table: "TblHamper",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_OrderDetailId",
                table: "TblHamper",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblOrderDetail_OrderDetailId",
                table: "TblHamper",
                column: "OrderDetailId",
                principalTable: "TblOrderDetail",
                principalColumn: "OrderDetailId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
