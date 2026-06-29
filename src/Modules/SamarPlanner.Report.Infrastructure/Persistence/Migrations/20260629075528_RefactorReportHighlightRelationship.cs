using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamarPlanner.Report.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorReportHighlightRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportReportHighlight",
                schema: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Reports_Id_UserId",
                schema: "Report",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_ReportHighlights_Id_UserId",
                schema: "Report",
                table: "ReportHighlights");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Report",
                table: "ReportHighlights",
                newName: "ReportId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Report",
                table: "Reports",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                schema: "Report",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportHighlights_ReportId",
                schema: "Report",
                table: "ReportHighlights",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportHighlights_Text",
                schema: "Report",
                table: "ReportHighlights",
                column: "Text");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportHighlights_Reports_ReportId",
                schema: "Report",
                table: "ReportHighlights",
                column: "ReportId",
                principalSchema: "Report",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportHighlights_Reports_ReportId",
                schema: "Report",
                table: "ReportHighlights");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId",
                schema: "Report",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_ReportHighlights_ReportId",
                schema: "Report",
                table: "ReportHighlights");

            migrationBuilder.DropIndex(
                name: "IX_ReportHighlights_Text",
                schema: "Report",
                table: "ReportHighlights");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                schema: "Report",
                table: "ReportHighlights",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Report",
                table: "Reports",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

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
                name: "IX_Reports_Id_UserId",
                schema: "Report",
                table: "Reports",
                columns: new[] { "Id", "UserId" });

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
        }
    }
}
