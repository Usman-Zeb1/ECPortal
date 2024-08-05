using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class removedtargetscolumnsfromthesalestbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeePrepaidSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeePostpaidSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeMWalletSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeFourGSales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeePrepaidSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeePostpaidSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeeMWalletSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeeFourGSales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
