using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class updatedRecognitionsmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRecognitions_Employee_EmployeeId",
                table: "EmployeeRecognitions");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeRecognitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "RecognizedBy",
                table: "EmployeeRecognitions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRecognitions_Employee_EmployeeId",
                table: "EmployeeRecognitions",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRecognitions_Employee_EmployeeId",
                table: "EmployeeRecognitions");

            migrationBuilder.DropColumn(
                name: "RecognizedBy",
                table: "EmployeeRecognitions");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "EmployeeRecognitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRecognitions_Employee_EmployeeId",
                table: "EmployeeRecognitions",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
