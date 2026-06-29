using MediatR;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Application.UseCases.Queries.GetReportDetail;

public class GetReportDetailQueryHandler(IReportRepository reportRepository)
    : IRequestHandler<GetReportDetailQuery, Result<GetReportDetailQueryResponse>>
{
    public async Task<Result<GetReportDetailQueryResponse>> Handle(GetReportDetailQuery request,
        CancellationToken cancellationToken)
    {
        var report =
            await reportRepository.GetByIdWithHighlightsAsync(request.ReportId, request.UserId, cancellationToken);
        if (report is null)
            return Result<GetReportDetailQueryResponse>.NotfoundFailure("گزارش یافت نشد.");

        return Result<GetReportDetailQueryResponse>.Success(
            new GetReportDetailQueryResponse(
                report.Title,
                report.Note,
                report.PeriodStart,
                report.PeriodEnd,
                report.Score,
                report.Highlights
                    .Select(h => new ReportHighlightDto(h.Text, h.Type))
                    .ToList()
            ));
    }
}