using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeAPI.Migrations
{
    /// <inheritdoc />
    public partial class TimePassed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimePassed",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TimePassed",
                table: "Tasks",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
