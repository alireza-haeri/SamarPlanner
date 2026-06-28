using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamarPlanner.Goal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoalType",
                schema: "Goal",
                table: "Goal",
                newName: "Status");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PeriodStart",
                schema: "Goal",
                table: "Goal",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PeriodEnd",
                schema: "Goal",
                table: "Goal",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                schema: "Goal",
                table: "Goal",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "RolledOverId",
                schema: "Goal",
                table: "Goal",
                type: "uniqueidentifier",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                schema: "Goal",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "RolledOverId",
                schema: "Goal",
                table: "Goal");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "Goal",
                table: "Goal",
                newName: "GoalType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodStart",
                schema: "Goal",
                table: "Goal",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PeriodEnd",
                schema: "Goal",
                table: "Goal",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
