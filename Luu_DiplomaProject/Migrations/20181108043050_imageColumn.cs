using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luu_DiplomaProject.Migrations
{
    public partial class imageColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblAddress");

            migrationBuilder.AddColumn<long>(
                name: "ContentSize",
                table: "TblHamper",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "TblHamper",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "TblHamper",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentSize",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "TblHamper");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "TblHamper");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TblAddress",
                nullable: false,
                defaultValue: "");
        }
    }
}
