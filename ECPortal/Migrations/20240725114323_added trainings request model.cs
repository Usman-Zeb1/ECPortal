using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class addedtrainingsrequestmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Business_Center",
                table: "EmployeeTargets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RCCH_Region",
                table: "EmployeeTargets",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TrainingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRequests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingRequests");

            migrationBuilder.DropColumn(
                name: "Business_Center",
                table: "EmployeeTargets");

            migrationBuilder.DropColumn(
                name: "RCCH_Region",
                table: "EmployeeTargets");
        }
    }
}
