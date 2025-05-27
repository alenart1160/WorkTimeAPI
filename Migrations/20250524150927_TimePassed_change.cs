using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeAPI.Migrations
{
    /// <inheritdoc />
    public partial class TimePassed_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePassed",
                table: "TaskTimers");

            migrationBuilder.AddColumn<string>(
                name: "TimePassed",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePassed",
                table: "Tasks");

            migrationBuilder.AddColumn<float>(
                name: "TimePassed",
                table: "TaskTimers",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
