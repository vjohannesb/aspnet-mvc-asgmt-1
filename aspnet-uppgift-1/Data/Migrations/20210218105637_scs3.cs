using Microsoft.EntityFrameworkCore.Migrations;

namespace aspnet_uppgift_1.Data.Migrations
{
    public partial class scs3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "SchoolClassStudents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents");

            migrationBuilder.AlterColumn<int>(
                name: "SchoolClassId",
                table: "SchoolClassStudents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolClassStudents_SchoolClasses_SchoolClassId",
                table: "SchoolClassStudents",
                column: "SchoolClassId",
                principalTable: "SchoolClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
