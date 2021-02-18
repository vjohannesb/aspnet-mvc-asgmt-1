using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnet_uppgift_1.Data.Migrations
{
    public partial class scs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SchoolClasses_SchoolClassId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SchoolClassId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SchoolClassId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "SchoolClasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolClassStudents",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SchoolClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClassStudents", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_SchoolClassStudents_AspNetUsers_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                        column: x => x.SchoolClassId,
                        principalTable: "SchoolClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_TeacherId",
                table: "SchoolClasses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassStudents_SchoolClassId",
                table: "SchoolClassStudents",
                column: "SchoolClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassStudents_StudentId1",
                table: "SchoolClassStudents",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClasses_AspNetUsers_TeacherId",
                table: "SchoolClasses",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClasses_AspNetUsers_TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropTable(
                name: "SchoolClassStudents");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SchoolClasses");

            migrationBuilder.AddColumn<int>(
                name: "SchoolClassId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SchoolClassId",
                table: "AspNetUsers",
                column: "SchoolClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SchoolClasses_SchoolClassId",
                table: "AspNetUsers",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
