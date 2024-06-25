using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Migrations
{
    public partial class ChangedTheKeysToCompositeKeyInTwoTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests");

            migrationBuilder.UpdateData(
                table: "ConnectionRequests",
                keyColumn: "RequestNotification",
                keyValue: null,
                column: "RequestNotification",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RequestNotification",
                table: "ConnectionRequests",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                columns: new[] { "EmployeeId", "FriendId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests",
                columns: new[] { "ReceiverId", "SenderId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Connections",
                table: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests");

            migrationBuilder.AlterColumn<string>(
                name: "RequestNotification",
                table: "ConnectionRequests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connections",
                table: "Connections",
                column: "EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConnectionRequests",
                table: "ConnectionRequests",
                column: "ReceiverId");
        }
    }
}
