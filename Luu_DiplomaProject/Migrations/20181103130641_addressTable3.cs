using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class addressTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAddress", x => x.AddressId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblCustomer_TblAddress_AddressId",
                table: "TblCustomer");

            migrationBuilder.DropTable(
                name: "TblAddress");

            migrationBuilder.DropIndex(
                name: "IX_TblCustomer_AddressId",
                table: "TblCustomer");
        }
    }
}
