using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_OP_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionAnwsers_CopyQuestion_SessionQuestionId",
                table: "SessionAnwsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_CopyQuestion_QuestionId",
                table: "SessionQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CopyQuestion",
                table: "CopyQuestion");

            migrationBuilder.RenameTable(
                name: "CopyQuestion",
                newName: "CopyQuestions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CopyQuestions",
                table: "CopyQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionAnwsers_CopyQuestions_SessionQuestionId",
                table: "SessionAnwsers",
                column: "SessionQuestionId",
                principalTable: "CopyQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_CopyQuestions_QuestionId",
                table: "SessionQuestions",
                column: "QuestionId",
                principalTable: "CopyQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionAnwsers_CopyQuestions_SessionQuestionId",
                table: "SessionAnwsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_CopyQuestions_QuestionId",
                table: "SessionQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CopyQuestions",
                table: "CopyQuestions");

            migrationBuilder.RenameTable(
                name: "CopyQuestions",
                newName: "CopyQuestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CopyQuestion",
                table: "CopyQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionAnwsers_CopyQuestion_SessionQuestionId",
                table: "SessionAnwsers",
                column: "SessionQuestionId",
                principalTable: "CopyQuestion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_CopyQuestion_QuestionId",
                table: "SessionQuestions",
                column: "QuestionId",
                principalTable: "CopyQuestion",
                principalColumn: "Id");
        }
    }
}
