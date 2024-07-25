using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class addedECforeignkeytoEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ECID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ECID",
                table: "Employee",
                column: "ECID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_ECs_ECID",
                table: "Employee",
                column: "ECID",
                principalTable: "ECs",
                principalColumn: "ECID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_ECs_ECID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ECID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ECID",
                table: "Employee");
        }
    }
}
