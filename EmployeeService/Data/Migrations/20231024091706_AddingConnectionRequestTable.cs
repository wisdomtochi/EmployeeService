using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    public partial class AddingConnectionRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionRequestEmployee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ConnectionRequests",
                newName: "SenderId");

            migrationBuilder.AddColumn<string>(
                name: "ConnectedNotification",
                table: "Connections",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "SenderId",
                table: "ConnectionRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "ConnectionRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ConnectionRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequests_EmployeeId",
                table: "ConnectionRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_Employees_EmployeeId",
                table: "ConnectionRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_Employees_EmployeeId",
                table: "ConnectionRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests");

            migrationBuilder.DropIndex(
                name: "IX_ConnectionRequests_EmployeeId",
                table: "ConnectionRequests");

            migrationBuilder.DropColumn(
                name: "ConnectedNotification",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "ConnectionRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ConnectionRequests");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "ConnectionRequests",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ConnectionRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConnectionRequestEmployee",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    RequestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionRequestEmployee", x => new { x.EmployeesId, x.RequestsId });
                    table.ForeignKey(
                        name: "FK_ConnectionRequestEmployee_ConnectionRequests_RequestsId",
                        column: x => x.RequestsId,
                        principalTable: "ConnectionRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionRequestEmployee_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestEmployee_RequestsId",
                table: "ConnectionRequestEmployee",
                column: "RequestsId");
        }
    }
}
