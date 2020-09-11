using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace DataAcces.Migrations
{
    public partial class Video1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PB_Foto_PB_FotoAlbum_PB_FotoalbumId",
                table: "PB_Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_PB_FotoAlbum_PB_Bruger_PB_BrugerId",
                table: "PB_FotoAlbum");

            migrationBuilder.AlterColumn<int>(
                name: "PB_BrugerId",
                table: "PB_FotoAlbum",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PB_FotoalbumId",
                table: "PB_Foto",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PB_Video",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    Beskrivelse = table.Column<string>(nullable: true),
                    PB_FotoalbumId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PB_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PB_Video_PB_FotoAlbum_PB_FotoalbumId",
                        column: x => x.PB_FotoalbumId,
                        principalTable: "PB_FotoAlbum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PB_Video_PB_FotoalbumId",
                table: "PB_Video",
                column: "PB_FotoalbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_PB_Foto_PB_FotoAlbum_PB_FotoalbumId",
                table: "PB_Foto",
                column: "PB_FotoalbumId",
                principalTable: "PB_FotoAlbum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PB_FotoAlbum_PB_Bruger_PB_BrugerId",
                table: "PB_FotoAlbum",
                column: "PB_BrugerId",
                principalTable: "PB_Bruger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PB_Foto_PB_FotoAlbum_PB_FotoalbumId",
                table: "PB_Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_PB_FotoAlbum_PB_Bruger_PB_BrugerId",
                table: "PB_FotoAlbum");

            migrationBuilder.DropTable(
                name: "PB_Video");

            migrationBuilder.AlterColumn<int>(
                name: "PB_BrugerId",
                table: "PB_FotoAlbum",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PB_FotoalbumId",
                table: "PB_Foto",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PB_Foto_PB_FotoAlbum_PB_FotoalbumId",
                table: "PB_Foto",
                column: "PB_FotoalbumId",
                principalTable: "PB_FotoAlbum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PB_FotoAlbum_PB_Bruger_PB_BrugerId",
                table: "PB_FotoAlbum",
                column: "PB_BrugerId",
                principalTable: "PB_Bruger",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
