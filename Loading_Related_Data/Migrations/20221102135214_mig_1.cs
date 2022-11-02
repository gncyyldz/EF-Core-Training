using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loading_Related_Data.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Ankara" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Yozgat" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Discriminator", "Name", "RegionId", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Employee", "Gençay", 1, 1500, "Yıldız" },
                    { 2, "Employee", "Mahmut", 2, 1500, "Yıldız" },
                    { 3, "Employee", "Rıfkı", 1, 1500, "Yıldız" },
                    { 4, "Employee", "Cüneyt", 2, 1500, "Yıldız" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "EmployeeId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4355) },
                    { 2, 1, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4368) },
                    { 3, 2, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4369) },
                    { 4, 2, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4369) },
                    { 5, 3, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4370) },
                    { 6, 3, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4371) },
                    { 7, 3, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4371) },
                    { 8, 4, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4372) },
                    { 9, 4, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4373) },
                    { 10, 1, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4374) },
                    { 11, 2, new DateTime(2022, 11, 2, 16, 52, 13, 833, DateTimeKind.Local).AddTicks(4374) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_RegionId",
                table: "Persons",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
