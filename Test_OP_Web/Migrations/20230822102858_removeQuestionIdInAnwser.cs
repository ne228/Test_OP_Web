using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_OP_Web.Migrations
{
    /// <inheritdoc />
    public partial class removeQuestionIdInAnwser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anwser_SessionQuestions_QuestionId1",
                table: "Anwser");

            migrationBuilder.RenameColumn(
                name: "QuestionId1",
                table: "Anwser",
                newName: "SessionQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Anwser_QuestionId1",
                table: "Anwser",
                newName: "IX_Anwser_SessionQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anwser_SessionQuestions_SessionQuestionId",
                table: "Anwser",
                column: "SessionQuestionId",
                principalTable: "SessionQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anwser_SessionQuestions_SessionQuestionId",
                table: "Anwser");

            migrationBuilder.RenameColumn(
                name: "SessionQuestionId",
                table: "Anwser",
                newName: "QuestionId1");

            migrationBuilder.RenameIndex(
                name: "IX_Anwser_SessionQuestionId",
                table: "Anwser",
                newName: "IX_Anwser_QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Anwser_SessionQuestions_QuestionId1",
                table: "Anwser",
                column: "QuestionId1",
                principalTable: "SessionQuestions",
                principalColumn: "Id");
        }
    }
}
