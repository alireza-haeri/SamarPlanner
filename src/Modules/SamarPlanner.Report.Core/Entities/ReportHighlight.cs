using SamarPlanner.Report.Core.Enums;

namespace SamarPlanner.Report.Core.Entities;

public class ReportHighlight
{
    public const string TableName = "ReportHighlights";

    public Guid Id { get; private init; }
    public string Text { get; private set; } = null!;
    public ReportHighlightType Type { get; private set; }

    public Guid ReportId { get; private set; }
    public Report Report { get; private set; } = null!;

    public static ReportHighlight Create(string text, ReportHighlightType type)
        => new()
        {
            Text = text,
            Type = type
        };
}