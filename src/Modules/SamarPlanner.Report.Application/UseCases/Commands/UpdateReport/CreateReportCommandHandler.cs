using MediatR;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Report.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Application.UseCases.Commands.UpdateReport;

public class UpdateReportCommandHandler(IReportRepository reportRepository)
    : IRequestHandler<UpdateReportCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var highlights = request.Highlights
            .Select(h => ReportHighlight.Create(h.Text, h.Type))
            .ToList();

        var report =
            await reportRepository.GetByIdWithHighlightsAsTrackingAsync(request.ReportId, request.UserId,
                cancellationToken);
        if (report is null)
            return Result<bool>.NotfoundFailure("گزارش یافت نشد");

        report.Update(request.Title, request.Note, request.Score, request.PeriodStart, request.PeriodEnd, highlights);

        var updateResult = await reportRepository.UpdateAsync(report, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure("خطا در به‌روزرسانی گزارش");

        return Result<bool>.Success(true);
    }
}