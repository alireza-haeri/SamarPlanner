using SamarPlanner.Report.Application.Contracts;

namespace SamarPlanner.Report.Application.Abstractions;

public interface IReportRepository
{
    Task<Guid?> CreateAsync(Core.Entities.Report report, CancellationToken cancellationToken = default);

    Task<Core.Entities.Report?> GetByIdWithHighlightsAsTrackingAsync(Guid reportId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Core.Entities.Report report, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Core.Entities.Report report, CancellationToken cancellationToken = default);

    Task<List<ReportSummary>>
        GetSummariesByUserIdAsync(Guid userId,DateOnly periodStart,DateOnly periodEnd, CancellationToken cancellationToken = default);

    Task<Core.Entities.Report?> GetByIdWithHighlightsAsync(Guid reportId, Guid userId, CancellationToken cancellationToken = default);
    Task<List<ReportSuggestions>> GetHighlightSuggestionsAsync(Guid userId, string text, CancellationToken cancellationToken = default);
}