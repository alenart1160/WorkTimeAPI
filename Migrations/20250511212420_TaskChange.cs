using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeAPI.Migrations
{
    /// <inheritdoc />
    public partial class TaskChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "TaskModel",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "TaskModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TaskModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TaskModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "TaskModel");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskModel");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskModel");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TaskModel",
                newName: "TaskName");
        }
    }
}
