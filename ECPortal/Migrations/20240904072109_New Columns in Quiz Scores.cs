using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pk.Com.Jazz.ECP.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnsinQuizScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FatalError",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizOnline",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizPercentage",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizTarget",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RamPercentage",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RamTarget",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsesCount",
                table: "QuizScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatalError",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "QuizOnline",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "QuizPercentage",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "QuizTarget",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "RamPercentage",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "RamTarget",
                table: "QuizScores");

            migrationBuilder.DropColumn(
                name: "ResponsesCount",
                table: "QuizScores");
        }
    }
}
