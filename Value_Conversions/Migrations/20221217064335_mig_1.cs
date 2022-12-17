using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ValueConversions.Migrations
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender2 = table.Column<int>(type: "int", nullable: false),
                    Married = table.Column<bool>(type: "bit", nullable: false),
                    Titles = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Gender", "Gender2", "Married", "Name", "Titles" },
                values: new object[,]
                {
                    { 1, "F", 1, true, "Ayşe", null },
                    { 2, "M", 0, false, "Hilmi", null },
                    { 3, "F", 1, true, "Raziye", null },
                    { 4, "M", 0, false, "Süleyman", null },
                    { 5, "F", 1, true, "Fadime", null },
                    { 6, "M", 0, true, "Şuayip", null },
                    { 7, "F", 1, false, "Lale", null },
                    { 8, "F", 1, true, "Jale", null },
                    { 9, "M", 0, true, "Rıfkı", null },
                    { 10, "M", 0, true, "Muaviye", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
