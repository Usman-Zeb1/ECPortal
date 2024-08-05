using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "EmployeeDeviceSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDeviceSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeFourGSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFourGSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMWalletSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMWalletSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePostpaidSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePostpaidSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePrepaidSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewSales = table.Column<int>(type: "int", nullable: false),
                    PrepaidMNP = table.Column<int>(type: "int", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePrepaidSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoxConversionSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoxConversionSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoxNewSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoxNewSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false),
                    EmployeePrepaidSaleId = table.Column<int>(type: "int", nullable: false),
                    EmployeePostpaidSaleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeDeviceSaleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeMWalletSaleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeFourGSaleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeRoxNewSaleId = table.Column<int>(type: "int", nullable: false),
                    EmployeeRoxConversionSaleId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeeDeviceSales_EmployeeDeviceSaleId",
                        column: x => x.EmployeeDeviceSaleId,
                        principalTable: "EmployeeDeviceSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeeFourGSales_EmployeeFourGSaleId",
                        column: x => x.EmployeeFourGSaleId,
                        principalTable: "EmployeeFourGSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeeMWalletSales_EmployeeMWalletSaleId",
                        column: x => x.EmployeeMWalletSaleId,
                        principalTable: "EmployeeMWalletSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeePostpaidSales_EmployeePostpaidSaleId",
                        column: x => x.EmployeePostpaidSaleId,
                        principalTable: "EmployeePostpaidSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeePrepaidSales_EmployeePrepaidSaleId",
                        column: x => x.EmployeePrepaidSaleId,
                        principalTable: "EmployeePrepaidSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeeRoxConversionSales_EmployeeRoxConversionSaleId",
                        column: x => x.EmployeeRoxConversionSaleId,
                        principalTable: "EmployeeRoxConversionSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_EmployeeRoxNewSales_EmployeeRoxNewSaleId",
                        column: x => x.EmployeeRoxNewSaleId,
                        principalTable: "EmployeeRoxNewSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSales_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeDeviceSaleId",
                table: "EmployeeSales",
                column: "EmployeeDeviceSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeFourGSaleId",
                table: "EmployeeSales",
                column: "EmployeeFourGSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeId",
                table: "EmployeeSales",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeMWalletSaleId",
                table: "EmployeeSales",
                column: "EmployeeMWalletSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeePostpaidSaleId",
                table: "EmployeeSales",
                column: "EmployeePostpaidSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeePrepaidSaleId",
                table: "EmployeeSales",
                column: "EmployeePrepaidSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeRoxConversionSaleId",
                table: "EmployeeSales",
                column: "EmployeeRoxConversionSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSales_EmployeeRoxNewSaleId",
                table: "EmployeeSales",
                column: "EmployeeRoxNewSaleId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ECAudits");

            migrationBuilder.DropTable(
                name: "ECGiveaways");

            migrationBuilder.DropTable(
                name: "ECStocks");

            migrationBuilder.DropTable(
                name: "ECTNAs");

            migrationBuilder.DropTable(
                name: "EmployeeCommissions");

            migrationBuilder.DropTable(
                name: "EmployeeEDAs");

            migrationBuilder.DropTable(
                name: "EmployeeFeedbacks");

            migrationBuilder.DropTable(
                name: "EmployeePerformances");

            migrationBuilder.DropTable(
                name: "EmployeeRecognitions");

            migrationBuilder.DropTable(
                name: "EmployeeSales");

            migrationBuilder.DropTable(
                name: "EmployeeTargets");

            migrationBuilder.DropTable(
                name: "EmployeeTrainings");

            migrationBuilder.DropTable(
                name: "QualityScores");

            migrationBuilder.DropTable(
                name: "QuizScores");

            migrationBuilder.DropTable(
                name: "TrainingRequests");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EmployeeDeviceSales");

            migrationBuilder.DropTable(
                name: "EmployeeFourGSales");

            migrationBuilder.DropTable(
                name: "EmployeeMWalletSales");

            migrationBuilder.DropTable(
                name: "EmployeePostpaidSales");

            migrationBuilder.DropTable(
                name: "EmployeePrepaidSales");

            migrationBuilder.DropTable(
                name: "EmployeeRoxConversionSales");

            migrationBuilder.DropTable(
                name: "EmployeeRoxNewSales");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ECs");
        }
    }
}
