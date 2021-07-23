using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentTeendanceBackend.Migrations
{
    public partial class RecordTableFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentRoll",
                table: "Record");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Record");

            migrationBuilder.AddColumn<string>(
                name: "StudentRoll",
                table: "Record",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
