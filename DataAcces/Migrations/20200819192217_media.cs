using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAcces.Migrations
{
    public partial class media : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OprettelseDato",
                table: "PB_Video");

            migrationBuilder.AddColumn<DateTime>(
                name: "OprettetDato",
                table: "PB_Video",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OprettetDato",
                table: "PB_Video");

            migrationBuilder.AddColumn<DateTime>(
                name: "OprettelseDato",
                table: "PB_Video",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
