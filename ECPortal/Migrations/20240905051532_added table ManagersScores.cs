using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class addedtableManagersScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManagersScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    asTarget = table.Column<int>(type: "int", nullable: false),
                    AgentSatisfaction = table.Column<int>(type: "int", nullable: false),
                    asPercentage = table.Column<int>(type: "int", nullable: false),
                    vsTarget = table.Column<int>(type: "int", nullable: false),
                    visitSatisfaction = table.Column<int>(type: "int", nullable: false),
                    vsPercentage = table.Column<int>(type: "int", nullable: false),
                    QuizTarget = table.Column<int>(type: "int", nullable: false),
                    QuizOnline = table.Column<int>(type: "int", nullable: false),
                    QuizPercentage = table.Column<int>(type: "int", nullable: false),
                    RamTarget = table.Column<int>(type: "int", nullable: false),
                    FatalError = table.Column<int>(type: "int", nullable: false),
                    RamPercentage = table.Column<int>(type: "int", nullable: false),
                    MSTarget = table.Column<int>(type: "int", nullable: false),
                    MysteryShopping = table.Column<int>(type: "int", nullable: false),
                    MSPercentage = table.Column<int>(type: "int", nullable: false),
                    ResponsesCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagersScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagersScores_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagersScores_EmployeeId",
                table: "ManagersScores",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagersScores");
        }
    }
}
