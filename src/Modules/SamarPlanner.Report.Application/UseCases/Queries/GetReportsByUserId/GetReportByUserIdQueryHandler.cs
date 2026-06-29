using MediatR;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Application.UseCases.Queries.GetReportsByUserId;

public class GetReportByUserIdQueryHandler(IReportRepository reportRepository)
    : IRequestHandler<GetReportsByUserIdQuery, Result<GetReportsByUserIdQueryResponse>>
{
    public async Task<Result<GetReportsByUserIdQueryResponse>> Handle(GetReportsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var reports = await reportRepository.GetSummariesByUserIdAsync(request.UserId, request.PeriodStart,
            request.PeriodEnd, cancellationToken);

        return Result<GetReportsByUserIdQueryResponse>.Success(new GetReportsByUserIdQueryResponse(
            reports.Select(r =>
                    new GetReportsByUserIdQueryResponseReport(
                        r.ReportId, 
                        r.Title, 
                        r.NotePreview,
                        r.PeriodStart,
                        r.PeriodEnd,
                        r.Score))
                .ToList()
        ));
    }
}