using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamarPlanner.Task.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "task");

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Priority = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ParentGoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DefaultTime = table.Column<TimeOnly>(type: "time", unicode: false, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", unicode: false, nullable: false),
                    RepeatPattern = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskOccurrence",
                schema: "task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", unicode: false, nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", unicode: false, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Score = table.Column<int>(type: "int", unicode: false, maxLength: 50, nullable: true),
                    IsSkipped = table.Column<bool>(type: "bit", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskOccurrence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskOccurrence_Task_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "task",
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_Id_UserId",
                schema: "task",
                table: "Task",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskOccurrence_TaskId",
                schema: "task",
                table: "TaskOccurrence",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskOccurrence",
                schema: "task");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "task");
        }
    }
}
