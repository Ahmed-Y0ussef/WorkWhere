using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Users_StudentId",
                table: "studentCourses");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Users_StudentId",
                table: "studentCourses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_Users_StudentId",
                table: "studentCourses");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Courses_CourseId",
                table: "studentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_Users_StudentId",
                table: "studentCourses",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
