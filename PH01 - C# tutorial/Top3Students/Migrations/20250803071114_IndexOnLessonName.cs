using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PH01___C__tutorial.Migrations
{
    /// <inheritdoc />
    public partial class IndexOnLessonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonName",
                table: "Lessons",
                column: "LessonName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lessons_LessonName",
                table: "Lessons");
        }
    }
}
