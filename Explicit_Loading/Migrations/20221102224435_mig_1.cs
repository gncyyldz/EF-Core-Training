using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Explicit_Loading.Migrations
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
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Regions_RegionId",
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
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
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
                table: "Employees",
                columns: new[] { "Id", "Name", "RegionId", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, "Gençay", 1, 1500, "Yıldız" },
                    { 2, "Mahmut", 2, 1500, "Yıldız" },
                    { 3, "Rıfkı", 1, 1500, "Yıldız" },
                    { 4, "Cüneyt", 2, 1500, "Yıldız" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "EmployeeId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1041) },
                    { 2, 1, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1050) },
                    { 3, 2, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1051) },
                    { 4, 2, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1052) },
                    { 5, 3, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1052) },
                    { 6, 3, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1053) },
                    { 7, 3, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1054) },
                    { 8, 4, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1055) },
                    { 9, 4, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1055) },
                    { 10, 1, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1056) },
                    { 11, 2, new DateTime(2022, 11, 3, 1, 44, 34, 861, DateTimeKind.Local).AddTicks(1056) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RegionId",
                table: "Employees",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
