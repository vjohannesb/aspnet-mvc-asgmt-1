using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnet_uppgift_1.Data.Migrations
{
    public partial class scs4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClasses_AspNetUsers_TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_AspNetUsers_StudentId",
                table: "SchoolClassStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClassStudents_StudentId",
                table: "SchoolClassStudents");

            migrationBuilder.DropIndex(
                name: "IX_SchoolClasses_TeacherId",
                table: "SchoolClasses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SchoolClassStudents");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "SchoolClassStudents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "SchoolClassStudents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "SchoolClasses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "SchoolClassStudents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "SchoolClassStudents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "SchoolClassStudents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "SchoolClasses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolClassStudents",
                table: "SchoolClassStudents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClassStudents_StudentId",
                table: "SchoolClassStudents",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolClasses_TeacherId",
                table: "SchoolClasses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClasses_AspNetUsers_TeacherId",
                table: "SchoolClasses",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_AspNetUsers_StudentId",
                table: "SchoolClassStudents",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
