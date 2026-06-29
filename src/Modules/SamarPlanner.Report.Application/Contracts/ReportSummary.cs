namespace SamarPlanner.Report.Application.Contracts;

public record ReportSummary(Guid ReportId,string? Title, string NotePreview, DateOnly PeriodStart, DateOnly PeriodEnd,int? Score);