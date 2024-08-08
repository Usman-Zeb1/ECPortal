using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class addedECTargetstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ECTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ECID = table.Column<int>(type: "int", nullable: false),
                    ECPrepaidSaleTarget = table.Column<int>(type: "int", nullable: false),
                    ECPostpaidSaleTarget = table.Column<int>(type: "int", nullable: false),
                    ECDeviceSaleTarget = table.Column<int>(type: "int", nullable: false),
                    ECMWalletSaleTarget = table.Column<int>(type: "int", nullable: false),
                    ECFourGSaleTarget = table.Column<int>(type: "int", nullable: false),
                    ECRoxNewSaleTarget = table.Column<int>(type: "int", nullable: false),
                    ECRoxConversionSaleTarget = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ECTargets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ECTargets_ECs_ECID",
                        column: x => x.ECID,
                        principalTable: "ECs",
                        principalColumn: "ECID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ECTargets_ECID",
                table: "ECTargets",
                column: "ECID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ECTargets");
        }
    }
}
