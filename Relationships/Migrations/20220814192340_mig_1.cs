using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relationships.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalisanAdresler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanAdresler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalisanAdresler_Calisanlar_Id",
                        column: x => x.Id,
                        principalTable: "Calisanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanAdresler");

            migrationBuilder.DropTable(
                name: "Calisanlar");
        }
    }
}
