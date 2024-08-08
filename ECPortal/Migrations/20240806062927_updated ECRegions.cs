using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class updatedECRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ECs_ECRegionID",
                table: "ECs",
                column: "ECRegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ECs_ECRegions_ECRegionID",
                table: "ECs",
                column: "ECRegionID",
                principalTable: "ECRegions",
                principalColumn: "ECRegionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ECs_ECRegions_ECRegionID",
                table: "ECs");

            migrationBuilder.DropIndex(
                name: "IX_ECs_ECRegionID",
                table: "ECs");
        }
    }
}
