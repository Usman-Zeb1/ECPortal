using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class updatedhierarchydatatypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter existing columns to apply MaxLength constraint
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employee",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserAdLogin",
                table: "Employee",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EditBy",
                table: "Employee",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DOJ",
                table: "Employee",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // Alter new columns
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CNIC",
                table: "Employee",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DateOfJoiningBC",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DateOfLeaving",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceIMIE",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PosIds",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PosName",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SalesId",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WaridSalesId",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TabsId",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MfsId",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SiebelId",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EficsId",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EficsId2",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QmaticLogin",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QmaticPowerLogin",
                table: "Employee",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Employee",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert changes made in the Up method

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DateOfJoiningBC",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DateOfLeaving",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DeviceIMIE",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PosIds",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PosName",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "WaridSalesId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "TabsId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "MfsId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SiebelId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EficsId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EficsId2",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "QmaticLogin",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "QmaticPowerLogin",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeName",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserAdLogin",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EditBy",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DOJ",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
