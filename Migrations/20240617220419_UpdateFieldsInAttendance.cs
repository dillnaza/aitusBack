using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aitus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsInAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeachersSubjects_Subjects_SubjectId",
                table: "TeachersSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachersSubjects_Teachers_TeacherId",
                table: "TeachersSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachersSubjects",
                table: "TeachersSubjects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Attendances");

            migrationBuilder.RenameTable(
                name: "TeachersSubjects",
                newName: "TeacherSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_TeachersSubjects_SubjectId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_SubjectId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AttendanceStudents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherSubjects",
                table: "TeacherSubjects",
                columns: new[] { "TeacherId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectId",
                table: "TeacherSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Subjects_SubjectId",
                table: "TeacherSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Teachers_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherSubjects",
                table: "TeacherSubjects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AttendanceStudents");

            migrationBuilder.RenameTable(
                name: "TeacherSubjects",
                newName: "TeachersSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_SubjectId",
                table: "TeachersSubjects",
                newName: "IX_TeachersSubjects_SubjectId");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachersSubjects",
                table: "TeachersSubjects",
                columns: new[] { "TeacherId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersSubjects_Subjects_SubjectId",
                table: "TeachersSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachersSubjects_Teachers_TeacherId",
                table: "TeachersSubjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
