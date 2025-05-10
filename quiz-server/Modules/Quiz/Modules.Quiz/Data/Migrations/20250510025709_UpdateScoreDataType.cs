using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Quiz.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScoreDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Score",
                schema: "quiz",
                table: "QuizAttempts",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                schema: "quiz",
                table: "QuizAttempts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
