using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_OP_Web.Migrations
{
    /// <inheritdoc />
    public partial class renameOptionQuestionsToSessionsQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anwser_OptionQuestion_SessionQuestionId",
                table: "Anwser");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionQuestion_Questions_QuestionId",
                table: "OptionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionQuestion_Sessions_SessionId",
                table: "OptionQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionQuestion",
                table: "OptionQuestion");

            migrationBuilder.RenameTable(
                name: "OptionQuestion",
                newName: "SessionQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_OptionQuestion_SessionId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionQuestion_QuestionId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_QuestionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStart",
                table: "Sessions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeFinsih",
                table: "Sessions",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Reports",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionQuestions",
                table: "SessionQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Anwser_SessionQuestions_SessionQuestionId",
                table: "Anwser",
                column: "SessionQuestionId",
                principalTable: "SessionQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Questions_QuestionId",
                table: "SessionQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Sessions_SessionId",
                table: "SessionQuestions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anwser_SessionQuestions_SessionQuestionId",
                table: "Anwser");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Questions_QuestionId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Sessions_SessionId",
                table: "SessionQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionQuestions",
                table: "SessionQuestions");

            migrationBuilder.RenameTable(
                name: "SessionQuestions",
                newName: "OptionQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_SessionId",
                table: "OptionQuestion",
                newName: "IX_OptionQuestion_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_QuestionId",
                table: "OptionQuestion",
                newName: "IX_OptionQuestion_QuestionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStart",
                table: "Sessions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeFinsih",
                table: "Sessions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Reports",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionQuestion",
                table: "OptionQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Anwser_OptionQuestion_SessionQuestionId",
                table: "Anwser",
                column: "SessionQuestionId",
                principalTable: "OptionQuestion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionQuestion_Questions_QuestionId",
                table: "OptionQuestion",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionQuestion_Sessions_SessionId",
                table: "OptionQuestion",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
