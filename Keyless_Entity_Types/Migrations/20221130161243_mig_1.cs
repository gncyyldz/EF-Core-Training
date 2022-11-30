using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KeylessEntityTypes.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Name" },
                values: new object[,]
                {
                    { 1, "Ayşe" },
                    { 2, "Hilmi" },
                    { 3, "Raziye" },
                    { 4, "Süleyman" },
                    { 5, "Fadime" },
                    { 6, "Şuayip" },
                    { 7, "Lale" },
                    { 8, "Jale" },
                    { 9, "Rıfkı" },
                    { 10, "Muaviye" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Description", "PersonId", "Price" },
                values: new object[,]
                {
                    { 1, "...", 1, 10 },
                    { 2, "...", 2, 20 },
                    { 3, "...", 4, 30 },
                    { 4, "...", 5, 40 },
                    { 5, "...", 1, 50 },
                    { 6, "...", 6, 60 },
                    { 7, "...", 7, 70 },
                    { 8, "...", 1, 80 },
                    { 9, "...", 8, 90 },
                    { 10, "...", 9, 100 },
                    { 11, "...", 1, 110 },
                    { 12, "...", 2, 120 },
                    { 13, "...", 2, 130 },
                    { 14, "...", 3, 140 },
                    { 15, "...", 1, 150 },
                    { 16, "...", 4, 160 },
                    { 17, "...", 1, 170 },
                    { 18, "...", 1, 180 },
                    { 19, "...", 5, 190 },
                    { 20, "...", 6, 200 },
                    { 21, "...", 1, 210 },
                    { 22, "...", 7, 220 },
                    { 23, "...", 7, 230 },
                    { 24, "...", 8, 240 },
                    { 25, "...", 1, 250 },
                    { 26, "...", 1, 260 },
                    { 27, "...", 9, 270 },
                    { 28, "...", 9, 280 },
                    { 29, "...", 9, 290 },
                    { 30, "...", 2, 300 },
                    { 31, "...", 3, 310 },
                    { 32, "...", 1, 320 },
                    { 33, "...", 1, 330 },
                    { 34, "...", 1, 340 },
                    { 35, "...", 5, 350 },
                    { 36, "...", 1, 360 },
                    { 37, "...", 5, 370 },
                    { 38, "...", 1, 380 },
                    { 39, "...", 1, 390 },
                    { 40, "...", 1, 400 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonId",
                table: "Orders",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
