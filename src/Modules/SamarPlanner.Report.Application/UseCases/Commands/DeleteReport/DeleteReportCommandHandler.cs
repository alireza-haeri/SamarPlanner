using MediatR;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Application.UseCases.Commands.DeleteReport;

public class DeleteReportCommandHandler(IReportRepository reportRepository)
    : IRequestHandler<DeleteReportCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdWithHighlightsAsTrackingAsync(request.ReportId, request.UserId,
                cancellationToken);
        if (report is null)
            return Result<bool>.GeneralFailure("گزارش یافت نشد");

        var deleteResult = await reportRepository.DeleteAsync(report, cancellationToken);
        if (!deleteResult)
            return Result<bool>.GeneralFailure("خطا در حذف گزارش");

        return Result<bool>.Success(true);
    }
}