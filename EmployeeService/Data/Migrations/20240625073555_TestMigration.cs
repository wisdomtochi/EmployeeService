using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "Pk_Connections_Id",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Connections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
