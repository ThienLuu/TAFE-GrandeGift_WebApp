using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class orderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCustomerHamper_TblCustomer_CustomerId",
                table: "TblCustomerHamper");

            migrationBuilder.DropForeignKey(
                name: "FK_TblCustomerHamper_TblHamper_HamperId",
                table: "TblCustomerHamper");

            migrationBuilder.DropIndex(
                name: "IX_TblCustomerHamper_CustomerId",
                table: "TblCustomerHamper");

            migrationBuilder.DropIndex(
                name: "IX_TblCustomerHamper_HamperId",
                table: "TblCustomerHamper");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "TblCustomerHamper");

            migrationBuilder.DropColumn(
                name: "HamperId",
                table: "TblCustomerHamper");

            migrationBuilder.CreateTable(
                name: "Order",
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
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_TblCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "TblCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
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
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetail_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_HamperId",
                table: "OrderDetail",
                column: "HamperId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "TblCustomerHamper",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HamperId",
                table: "TblCustomerHamper",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblCustomerHamper_CustomerId",
                table: "TblCustomerHamper",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCustomerHamper_HamperId",
                table: "TblCustomerHamper",
                column: "HamperId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblCustomerHamper_TblCustomer_CustomerId",
                table: "TblCustomerHamper",
                column: "CustomerId",
                principalTable: "TblCustomer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TblCustomerHamper_TblHamper_HamperId",
                table: "TblCustomerHamper",
                column: "HamperId",
                principalTable: "TblHamper",
                principalColumn: "HamperId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
