using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class updatedECAudits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "isEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "isEnabled",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_AppUserId",
                table: "Employee",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
