using SamarPlanner.Shared.Contracts.Contracts;

namespace SamarPlanner.Report.Contracts;

public record CreateReportRequest(
    string? Title,
    string Note,
    int? Score,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    List<ReportHighlightDto> Highlights);