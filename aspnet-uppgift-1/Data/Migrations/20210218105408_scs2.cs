using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnet_uppgift_1.Data.Migrations
{
    public partial class scs2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_AspNetUsers_StudentId1",
                table: "SchoolClassStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClassStudents_StudentId1",
                table: "SchoolClassStudents");

            migrationBuilder.RenameColumn(
                name: "StudentId1",
                table: "SchoolClassStudents",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "SchoolClassStudents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassStudents_StudentId",
                table: "SchoolClassStudents",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_AspNetUsers_StudentId",
                table: "SchoolClassStudents",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_AspNetUsers_StudentId",
                table: "SchoolClassStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClassStudents_StudentId",
                table: "SchoolClassStudents");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SchoolClassStudents",
                newName: "StudentId1");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "SchoolClassStudents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassStudents_StudentId1",
                table: "SchoolClassStudents",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_AspNetUsers_StudentId1",
                table: "SchoolClassStudents",
                column: "StudentId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
