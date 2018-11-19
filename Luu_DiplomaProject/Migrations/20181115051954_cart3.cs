using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class cart3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_TblCustomer_CustomerId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_Cart_CartId",
                table: "TblHamper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "TblCart");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_CustomerId",
                table: "TblCart",
                newName: "IX_TblCart_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblCart",
                table: "TblCart",
                column: "CartId");

            migrationBuilder.CreateTable(
                name: "TblOrder",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    OrderDateTime = table.Column<DateTime>(nullable: false),
                    OrderPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrder", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "TblOrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    HamperId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrderDetail", x => x.OrderDetailId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_OrderDetailId",
                table: "TblHamper",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCart_TblCustomer_CustomerId",
                table: "TblCart",
                column: "CustomerId",
                principalTable: "TblCustomer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblCart_CartId",
                table: "TblHamper",
                column: "CartId",
                principalTable: "TblCart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_TblOrderDetail_OrderDetailId",
                table: "TblHamper",
                column: "OrderDetailId",
                principalTable: "TblOrderDetail",
                principalColumn: "OrderDetailId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCart_TblCustomer_CustomerId",
                table: "TblCart");

            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblCart_CartId",
                table: "TblHamper");

            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_TblOrderDetail_OrderDetailId",
                table: "TblHamper");

            migrationBuilder.DropTable(
                name: "TblOrder");

            migrationBuilder.DropTable(
                name: "TblOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_TblHamper_OrderDetailId",
                table: "TblHamper");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblCart",
                table: "TblCart");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "TblHamper");

            migrationBuilder.RenameTable(
                name: "TblCart",
                newName: "Cart");

            migrationBuilder.RenameIndex(
                name: "IX_TblCart_CustomerId",
                table: "Cart",
                newName: "IX_Cart_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_TblCustomer_CustomerId",
                table: "Cart",
                column: "CustomerId",
                principalTable: "TblCustomer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_Cart_CartId",
                table: "TblHamper",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
