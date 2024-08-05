using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class saletblchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Target",
                table: "EmployeeRoxConversionSales");

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "EmployeePostpaidSales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "EmployeePostpaidSales");

            migrationBuilder.AddColumn<int>(
                name: "Target",
                table: "EmployeeRoxConversionSales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
