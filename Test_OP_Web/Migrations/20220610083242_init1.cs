using Microsoft.EntityFrameworkCore.Migrations;

namespace Test_OP_Web.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anwser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Right = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anwser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    NumVar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.NumVar);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OneId = table.Column<int>(type: "int", nullable: true),
                    TwoId = table.Column<int>(type: "int", nullable: true),
                    ThreeId = table.Column<int>(type: "int", nullable: true),
                    FourId = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionNumVar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Anwser_FourId",
                        column: x => x.FourId,
                        principalTable: "Anwser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Anwser_OneId",
                        column: x => x.OneId,
                        principalTable: "Anwser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Anwser_ThreeId",
                        column: x => x.ThreeId,
                        principalTable: "Anwser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Anwser_TwoId",
                        column: x => x.TwoId,
                        principalTable: "Anwser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Options_OptionNumVar",
                        column: x => x.OptionNumVar,
                        principalTable: "Options",
                        principalColumn: "NumVar",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_FourId",
                table: "Question",
                column: "FourId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_OneId",
                table: "Question",
                column: "OneId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_OptionNumVar",
                table: "Question",
                column: "OptionNumVar");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ThreeId",
                table: "Question",
                column: "ThreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TwoId",
                table: "Question",
                column: "TwoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Anwser");

            migrationBuilder.DropTable(
                name: "Options");
        }
    }
}
