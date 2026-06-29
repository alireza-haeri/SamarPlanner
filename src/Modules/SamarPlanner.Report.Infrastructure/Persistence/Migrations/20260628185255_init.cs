using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamarPlanner.Report.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Report");

            migrationBuilder.CreateTable(
                name: "ReportHighlights",
                schema: "Report",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportHighlights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "Report",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Score = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportReportHighlight",
                schema: "Report",
                columns: table => new
                {
                    HighlightsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReportHighlight", x => new { x.HighlightsId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_ReportReportHighlight_ReportHighlights_HighlightsId",
                        column: x => x.HighlightsId,
                        principalSchema: "Report",
                        principalTable: "ReportHighlights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportReportHighlight_Reports_ReportsId",
                        column: x => x.ReportsId,
                        principalSchema: "Report",
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportHighlights_Id_UserId",
                schema: "Report",
                table: "ReportHighlights",
                columns: new[] { "Id", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ReportReportHighlight_ReportsId",
                schema: "Report",
                table: "ReportReportHighlight",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Id_UserId",
                schema: "Report",
                table: "Reports",
                columns: new[] { "Id", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportReportHighlight",
                schema: "Report");

            migrationBuilder.DropTable(
                name: "ReportHighlights",
                schema: "Report");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "Report");
        }
    }
}
