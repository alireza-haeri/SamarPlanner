using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamarPlanner.Task.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovSoftDeltedFieldOfTaskOccurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                schema: "task",
                table: "TaskOccurrence");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                schema: "task",
                table: "TaskOccurrence",
                type: "bit",
                unicode: false,
                nullable: false,
                defaultValue: false);
        }
    }
}
