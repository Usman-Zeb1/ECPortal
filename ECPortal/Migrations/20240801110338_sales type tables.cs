using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class salestypetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasicVibe",
                table: "EmployeeRoxNewSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Boost",
                table: "EmployeeRoxNewSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CrazyVibe",
                table: "EmployeeRoxNewSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsaneVibe",
                table: "EmployeeRoxNewSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "EmployeeRoxNewSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BasicVibe",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CrazyVibe",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsaneVibe",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoVibe",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreshSales",
                table: "EmployeePostpaidSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortIN",
                table: "EmployeePostpaidSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreToPost",
                table: "EmployeePostpaidSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RedRedZ",
                table: "EmployeePostpaidSales",
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
                name: "Total",
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

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "EmployeeFourGSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Handsets",
                table: "EmployeeDeviceSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MBB",
                table: "EmployeeDeviceSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeeDeviceSales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicVibe",
                table: "EmployeeRoxNewSales");

            migrationBuilder.DropColumn(
                name: "Boost",
                table: "EmployeeRoxNewSales");

            migrationBuilder.DropColumn(
                name: "CrazyVibe",
                table: "EmployeeRoxNewSales");

            migrationBuilder.DropColumn(
                name: "InsaneVibe",
                table: "EmployeeRoxNewSales");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "EmployeeRoxNewSales");

            migrationBuilder.DropColumn(
                name: "BasicVibe",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.DropColumn(
                name: "CrazyVibe",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.DropColumn(
                name: "InsaneVibe",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.DropColumn(
                name: "NoVibe",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.DropColumn(
                name: "FreshSales",
                table: "EmployeePostpaidSales");

            migrationBuilder.DropColumn(
                name: "PortIN",
                table: "EmployeePostpaidSales");

            migrationBuilder.DropColumn(
                name: "PreToPost",
                table: "EmployeePostpaidSales");

            migrationBuilder.DropColumn(
                name: "RedRedZ",
                table: "EmployeePostpaidSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeePostpaidSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeMWalletSales");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "EmployeeMWalletSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeFourGSales");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "EmployeeFourGSales");

            migrationBuilder.DropColumn(
                name: "Handsets",
                table: "EmployeeDeviceSales");

            migrationBuilder.DropColumn(
                name: "MBB",
                table: "EmployeeDeviceSales");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeDeviceSales");
        }
    }
}
