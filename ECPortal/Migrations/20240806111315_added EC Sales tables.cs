using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class addedECSalestables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECDeviceSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MBB = table.Column<int>(type: "int", nullable: false),
                    Handsets = table.Column<int>(type: "int", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECDeviceSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECFourGSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECFourGSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECMWalletSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECMWalletSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECPostpaidSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreshSales = table.Column<int>(type: "int", nullable: false),
                    PortIN = table.Column<int>(type: "int", nullable: false),
                    PreToPost = table.Column<int>(type: "int", nullable: false),
                    RedRedZ = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECPostpaidSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECPrepaidSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewSales = table.Column<int>(type: "int", nullable: false),
                    PrepaidMNP = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECPrepaidSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECRoxConversionSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicVibe = table.Column<int>(type: "int", nullable: false),
                    CrazyVibe = table.Column<int>(type: "int", nullable: false),
                    InsaneVibe = table.Column<int>(type: "int", nullable: false),
                    NoVibe = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECRoxConversionSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECRoxNewSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicVibe = table.Column<int>(type: "int", nullable: false),
                    Boost = table.Column<int>(type: "int", nullable: false),
                    CrazyVibe = table.Column<int>(type: "int", nullable: false),
                    InsaneVibe = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECRoxNewSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ECSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ECID = table.Column<int>(type: "int", nullable: false),
                    ECPrepaidSaleId = table.Column<int>(type: "int", nullable: false),
                    ECPostpaidSaleId = table.Column<int>(type: "int", nullable: false),
                    ECDeviceSaleId = table.Column<int>(type: "int", nullable: false),
                    ECMWalletSaleId = table.Column<int>(type: "int", nullable: false),
                    ECFourGSaleId = table.Column<int>(type: "int", nullable: false),
                    ECRoxNewSaleId = table.Column<int>(type: "int", nullable: false),
                    ECRoxConversionSaleId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SalesDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECSales_ECDeviceSales_ECDeviceSaleId",
                        column: x => x.ECDeviceSaleId,
                        principalTable: "ECDeviceSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECFourGSales_ECFourGSaleId",
                        column: x => x.ECFourGSaleId,
                        principalTable: "ECFourGSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECMWalletSales_ECMWalletSaleId",
                        column: x => x.ECMWalletSaleId,
                        principalTable: "ECMWalletSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECPostpaidSales_ECPostpaidSaleId",
                        column: x => x.ECPostpaidSaleId,
                        principalTable: "ECPostpaidSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECPrepaidSales_ECPrepaidSaleId",
                        column: x => x.ECPrepaidSaleId,
                        principalTable: "ECPrepaidSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECRoxConversionSales_ECRoxConversionSaleId",
                        column: x => x.ECRoxConversionSaleId,
                        principalTable: "ECRoxConversionSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECRoxNewSales_ECRoxNewSaleId",
                        column: x => x.ECRoxNewSaleId,
                        principalTable: "ECRoxNewSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ECSales_ECs_ECID",
                        column: x => x.ECID,
                        principalTable: "ECs",
                        principalColumn: "ECID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECDeviceSaleId",
                table: "ECSales",
                column: "ECDeviceSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECFourGSaleId",
                table: "ECSales",
                column: "ECFourGSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECID",
                table: "ECSales",
                column: "ECID");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECMWalletSaleId",
                table: "ECSales",
                column: "ECMWalletSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECPostpaidSaleId",
                table: "ECSales",
                column: "ECPostpaidSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECPrepaidSaleId",
                table: "ECSales",
                column: "ECPrepaidSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECRoxConversionSaleId",
                table: "ECSales",
                column: "ECRoxConversionSaleId");

            migrationBuilder.CreateIndex(
                name: "IX_ECSales_ECRoxNewSaleId",
                table: "ECSales",
                column: "ECRoxNewSaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECSales");

            migrationBuilder.DropTable(
                name: "ECDeviceSales");

            migrationBuilder.DropTable(
                name: "ECFourGSales");

            migrationBuilder.DropTable(
                name: "ECMWalletSales");

            migrationBuilder.DropTable(
                name: "ECPostpaidSales");

            migrationBuilder.DropTable(
                name: "ECPrepaidSales");

            migrationBuilder.DropTable(
                name: "ECRoxConversionSales");

            migrationBuilder.DropTable(
                name: "ECRoxNewSales");
        }
    }
}
