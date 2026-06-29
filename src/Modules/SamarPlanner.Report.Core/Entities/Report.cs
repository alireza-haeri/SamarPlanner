using SamarPlanner.Shared.Contracts.Contracts;

namespace SamarPlanner.Report.Core.Entities;

public class Report
{
    public const string TableName = "Reports";
    public const int NotePreviewLength = 50;
    public const int MaxSuggestionCount = 10;

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string? Title { get; private set; }
    public string Note { get; private set; } = null!;
    public int? Score { get; private set; }
    public DateOnly PeriodStart { get; private set; }
    public DateOnly PeriodEnd { get; private set; }

    private readonly List<ReportHighlight> _highlights = [];
    public IReadOnlyCollection<ReportHighlight> Highlights => _highlights.AsReadOnly();

    public static Report Create(string? title, string note, int? score, DateOnly periodStart, DateOnly periodEnd,
        List<ReportHighlight> highlights, Guid userId)
    {
        var report = new Report
        {
            Title = title,
            Note = note,
            Score = score,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            UserId = userId
        };

        report._highlights.AddRange(highlights);
        return report;
    }

    public void Update(string? title, string note, int? score, DateOnly periodStart, DateOnly periodEnd, List<ReportHighlight> highlights)
    {
        Title = title;
        Note = note;
        Score = score;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;

        _highlights.Clear();
        _highlights.AddRange(highlights);
    }
}