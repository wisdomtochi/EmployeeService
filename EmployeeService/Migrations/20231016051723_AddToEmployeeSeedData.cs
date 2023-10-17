using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    public partial class AddToEmployeeSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { 2, "Miselyn", "F", "Kisera", 847300 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { 3, "Jurome", "M", "Anthony", 324300 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { 1, "James", "M", "Uzuzu", 2847000 });
        }
    }
}
