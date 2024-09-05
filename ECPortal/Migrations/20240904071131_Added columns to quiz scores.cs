using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class Addedcolumnstoquizscores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "AgentSatisfaction",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "asPercentage",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "asTarget",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "visitSatisfaction",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "vsPercentage",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "vsTarget",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgentSatisfaction",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "asPercentage",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "asTarget",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "visitSatisfaction",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "vsPercentage",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "vsTarget",
                table: "QuizScores");

          
        }
    }
}
