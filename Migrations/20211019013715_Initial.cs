using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trackity.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Cost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "ExpenseId", "Cost", "Date", "Name" },
                values: new object[,]
                {
                    { 1, 600.0, new DateTime(2021, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rent Payment" },
                    { 2, 8.9900000000000002, new DateTime(2021, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netflix" },
                    { 3, 42.009999999999998, new DateTime(2021, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gas" },
                    { 4, 21.850000000000001, new DateTime(2021, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bubba's" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");
        }
    }
}
