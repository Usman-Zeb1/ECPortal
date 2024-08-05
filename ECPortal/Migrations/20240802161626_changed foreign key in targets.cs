using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class changedforeignkeyintargets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSales_Employee_EmployeeId",
                table: "EmployeeSales");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSales_EmployeeId",
                table: "EmployeeSales");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeSales");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeNumber",
                table: "EmployeeSales",
                column: "EmployeeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeNumber",
                table: "Employee",
                column: "EmployeeNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSales_Employee_EmployeeNumber",
                table: "EmployeeSales",
                column: "EmployeeNumber",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSales_Employee_EmployeeNumber",
                table: "EmployeeSales");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSales_EmployeeNumber",
                table: "EmployeeSales");

            migrationBuilder.DropIndex(
                name: "IX_Employee_EmployeeNumber",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeeSales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeId",
                table: "EmployeeSales",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSales_Employee_EmployeeId",
                table: "EmployeeSales",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId");
        }
    }
}
