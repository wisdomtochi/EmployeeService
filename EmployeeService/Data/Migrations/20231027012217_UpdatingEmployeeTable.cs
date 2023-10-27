using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    public partial class UpdatingEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Connections_ConnectionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeEmployee");

            migrationBuilder.RenameColumn(
                name: "ConnectionId",
                table: "Employees",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ConnectionId",
                table: "Employees",
                newName: "IX_Employees_EmployeeId");

            migrationBuilder.CreateTable(
                name: "ConnectionEmployee",
                columns: table => new
                {
                    ConnectionsId = table.Column<int>(type: "int", nullable: false),
                    EmployeesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionEmployee", x => new { x.ConnectionsId, x.EmployeesId });
                    table.ForeignKey(
                        name: "FK_ConnectionEmployee_Connections_ConnectionsId",
                        column: x => x.ConnectionsId,
                        principalTable: "Connections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionEmployee_EmployeesId",
                table: "ConnectionEmployee",
                column: "EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmployeeId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "ConnectionEmployee");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Employees",
                newName: "ConnectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                newName: "IX_Employees_ConnectionId");

            migrationBuilder.CreateTable(
                name: "EmployeeEmployee",
                columns: table => new
                {
                    ConnectionsId = table.Column<int>(type: "int", nullable: false),
                    RequestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEmployee", x => new { x.ConnectionsId, x.RequestsId });
                    table.ForeignKey(
                        name: "FK_EmployeeEmployee_Employees_ConnectionsId",
                        column: x => x.ConnectionsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeEmployee_Employees_RequestsId",
                        column: x => x.RequestsId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEmployee_RequestsId",
                table: "EmployeeEmployee",
                column: "RequestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Connections_ConnectionId",
                table: "Employees",
                column: "ConnectionId",
                principalTable: "Connections",
                principalColumn: "Id");
        }
    }
}
