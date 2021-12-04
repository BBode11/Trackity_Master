using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trackity.Migrations
{
    public partial class DepositCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Expenses",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    DepositId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositId);
                });

            migrationBuilder.InsertData(
                table: "Deposits",
                columns: new[] { "DepositId", "Amount", "Date", "Name" },
                values: new object[,]
                {
                    { 1, 1500.0, new DateTime(2021, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pay Check" },
                    { 2, 1500.0, new DateTime(2021, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pay Check" },
                    { 3, 500.0, new DateTime(2021, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Scratch Off Ticket" },
                    { 4, 47.5, new DateTime(2021, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stock Returns" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
