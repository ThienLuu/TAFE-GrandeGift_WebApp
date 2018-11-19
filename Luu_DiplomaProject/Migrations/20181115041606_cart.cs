using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "TblHamper");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "TblHamper",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDetails",
                table: "TblHamper",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<int>(nullable: false),
                    HamperId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_TblCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "TblCustomer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_CartId",
                table: "TblHamper",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHamper_Cart_CartId",
                table: "TblHamper",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHamper_Cart_CartId",
                table: "TblHamper");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_TblHamper_CartId",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "OrderDetails",
                table: "TblHamper");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "TblHamper",
                nullable: true);

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
                    HamperId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
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
    }
}
