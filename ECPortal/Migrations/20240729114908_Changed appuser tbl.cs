using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class Changedappusertbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEnabled",
                table: "AspNetUsers");
        }
    }
}
