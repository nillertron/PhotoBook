using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace DataAcces.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PB_Bruger",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SharedId = table.Column<string>(nullable: true),
                    Navn = table.Column<string>(nullable: true),
                    EfterNavn = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PB_Bruger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PB_FotoAlbum",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Navn = table.Column<string>(nullable: true),
                    Beskrivelse = table.Column<string>(nullable: true),
                    OprettetDato = table.Column<DateTime>(nullable: false),
                    PB_BrugerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PB_FotoAlbum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PB_FotoAlbum_PB_Bruger_PB_BrugerId",
                        column: x => x.PB_BrugerId,
                        principalTable: "PB_Bruger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PB_Foto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Beskrivelse = table.Column<string>(nullable: true),
                    OprettetDato = table.Column<DateTime>(nullable: false),
                    PB_FotoalbumId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PB_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PB_Foto_PB_FotoAlbum_PB_FotoalbumId",
                        column: x => x.PB_FotoalbumId,
                        principalTable: "PB_FotoAlbum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PB_Foto_PB_FotoalbumId",
                table: "PB_Foto",
                column: "PB_FotoalbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PB_FotoAlbum_PB_BrugerId",
                table: "PB_FotoAlbum",
                column: "PB_BrugerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PB_Foto");

            migrationBuilder.DropTable(
                name: "PB_FotoAlbum");

            migrationBuilder.DropTable(
                name: "PB_Bruger");
        }
    }
}
