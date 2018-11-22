using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class addressAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "TblOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderDetail_OrderId",
                table: "TblOrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrder_AddressId",
                table: "TblOrder",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrder_TblAddress_AddressId",
                table: "TblOrder",
                column: "AddressId",
                principalTable: "TblAddress",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblOrderDetail_TblOrder_OrderId",
                table: "TblOrderDetail",
                column: "OrderId",
                principalTable: "TblOrder",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrder_TblAddress_AddressId",
                table: "TblOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_TblOrderDetail_TblOrder_OrderId",
                table: "TblOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TblOrderDetail_OrderId",
                table: "TblOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TblOrder_AddressId",
                table: "TblOrder");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "TblOrder");
        }
    }
}
