using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    public partial class AlterEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { "Jurome", "M", "Anthony", 324300 });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { "Doseel", "F", "Paul", 332300 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { 1, "Miselyn", "F", "Kisera", 847300 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { "Miselyn", "F", "Kisera", 847300 });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FirstName", "Gender", "LastName", "Salary" },
                values: new object[] { "Jurome", "M", "Anthony", 324300 });
        }
    }
}
