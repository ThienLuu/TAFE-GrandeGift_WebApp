using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class addressAndOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblOrder_TblAddress_AddressId",
                table: "TblOrder");

            migrationBuilder.DropIndex(
                name: "IX_TblOrder_AddressId",
                table: "TblOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
