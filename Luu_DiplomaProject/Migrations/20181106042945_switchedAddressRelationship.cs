using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class switchedAddressRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCustomer_TblAddress_AddressId",
                table: "TblCustomer");

            migrationBuilder.DropIndex(
                name: "IX_TblCustomer_AddressId",
                table: "TblCustomer");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "TblCustomer");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "TblAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TblAddress",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TblAddress_CustomerId",
                table: "TblAddress",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblAddress_TblCustomer_CustomerId",
                table: "TblAddress",
                column: "CustomerId",
                principalTable: "TblCustomer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblAddress_TblCustomer_CustomerId",
                table: "TblAddress");

            migrationBuilder.DropIndex(
                name: "IX_TblAddress_CustomerId",
                table: "TblAddress");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "TblAddress");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblAddress");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "TblCustomer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblCustomer_AddressId",
                table: "TblCustomer",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCustomer_TblAddress_AddressId",
                table: "TblCustomer",
                column: "AddressId",
                principalTable: "TblAddress",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
