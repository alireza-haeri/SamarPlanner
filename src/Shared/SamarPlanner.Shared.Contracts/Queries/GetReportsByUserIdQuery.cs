#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetReportsByUserIdQuery(Guid UserId, DateOnly PeriodStart, DateOnly PeriodEnd)
    : IRequest<Result<GetReportsByUserIdQueryResponse>>;

public record GetReportsByUserIdQueryResponse(List<GetReportsByUserIdQueryResponseReport> Reports);

public record GetReportsByUserIdQueryResponseReport(
    Guid ReportId,
    string? Title,
    string Note,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    int? Score);