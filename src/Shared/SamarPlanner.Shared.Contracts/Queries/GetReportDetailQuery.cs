#region

using MediatR;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetReportDetailQuery(Guid ReportId, Guid UserId) : IRequest<Result<GetReportDetailQueryResponse>>;

public record GetReportDetailQueryResponse(
    string? Title,
    string Note,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    int? Score,
    List<ReportHighlightDto> Highlights);