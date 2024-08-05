using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class updatedtargetstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTargets_Employee_EmployeeId",
                table: "EmployeeTargets");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTargets_EmployeeId",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "Business_Center",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "RCCH_Region",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "TargetAmount",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "TargetEndDate",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "TargetStartDate",
                table: "EmployeeTargets");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeTargets",
                newName: "Year");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDeviceSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeFourGSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeMWalletSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeNumber",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeePostpaidSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeePrepaidSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeRoxConversionSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeRoxNewSaleTarget",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "EmployeeTargets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTargets_EmployeeNumber",
                table: "EmployeeTargets",
                column: "EmployeeNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTargets_Employee_EmployeeNumber",
                table: "EmployeeTargets",
                column: "EmployeeNumber",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTargets_Employee_EmployeeNumber",
                table: "EmployeeTargets");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTargets_EmployeeNumber",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeeDeviceSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeeFourGSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeeMWalletSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeePostpaidSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeePrepaidSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeeRoxConversionSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "EmployeeRoxNewSaleTarget",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "EmployeeTargets");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "EmployeeTargets",
                newName: "EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Business_Center",
                table: "EmployeeTargets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "EmployeeTargets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RCCH_Region",
                table: "EmployeeTargets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TargetAmount",
                table: "EmployeeTargets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetEndDate",
                table: "EmployeeTargets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetStartDate",
                table: "EmployeeTargets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTargets_EmployeeId",
                table: "EmployeeTargets",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTargets_Employee_EmployeeId",
                table: "EmployeeTargets",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
