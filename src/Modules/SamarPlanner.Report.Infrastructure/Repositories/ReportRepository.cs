using Microsoft.EntityFrameworkCore;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Report.Application.Contracts;
using SamarPlanner.Report.Infrastructure.Persistence;

namespace SamarPlanner.Report.Infrastructure.Repositories;

public class ReportRepository(ReportDbContext context) : IReportRepository
{
    public async Task<Guid?> CreateAsync(Core.Entities.Report report, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.Reports.AddAsync(report, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return report.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Core.Entities.Report?> GetByIdWithHighlightsAsTrackingAsync(Guid reportId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Reports
            .AsTracking()
            .Include(r => r.Highlights)
            .FirstOrDefaultAsync(r => r.Id == reportId && r.UserId == userId, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Core.Entities.Report report, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Update(report);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Core.Entities.Report report, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Remove(report);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<List<ReportSummary>> GetSummariesByUserIdAsync(Guid userId, DateOnly periodStart,
        DateOnly periodEnd,
        CancellationToken cancellationToken = default)
    {
        var notePreviewLenght = Core.Entities.Report.NotePreviewLength;

        return await context.Reports
            .AsNoTracking()
            .Where(r => r.UserId == userId && r.PeriodStart <= periodStart && r.PeriodEnd >= periodEnd)
            .Select(r => new ReportSummary(
                ReportId: r.Id,
                Title: r.Title,
                NotePreview: (r.Note.Length > notePreviewLenght)
                    ? r.Note[..(notePreviewLenght + 1)] + "..."
                    : r.Note,
                PeriodStart: r.PeriodStart,
                PeriodEnd: r.PeriodEnd,
                Score: r.Score)
            )
            .ToListAsync(cancellationToken);
    }

    public async Task<Core.Entities.Report?> GetByIdWithHighlightsAsync(Guid reportId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Reports
            .AsNoTracking()
            .Include(r => r.Highlights)
            .FirstOrDefaultAsync(r => r.Id == reportId && r.UserId == userId, cancellationToken);
    }

    public async Task<List<ReportSuggestions>> GetHighlightSuggestionsAsync(Guid userId, string text,
        CancellationToken cancellationToken = default)
    {
        return await context.Highlights
            .AsNoTracking()
            .Where(h => h.Report.UserId == userId && h.Text.Contains(text))
            .Select(h => new ReportSuggestions(h.Text, h.Type))
            .DistinctBy(h => new ReportSuggestions(h.Text, h.Type))
            .Take(Core.Entities.Report.MaxSuggestionCount)
            .ToListAsync(cancellationToken);
    }
}