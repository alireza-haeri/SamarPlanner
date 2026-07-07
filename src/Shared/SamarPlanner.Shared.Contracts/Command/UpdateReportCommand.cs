#region

using MediatR;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record UpdateReportCommand(
    Guid UserId,
    Guid ReportId,
    string? Title,
    string Note,
    int? Score,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    List<ReportHighlightDto> Highlights) : IRequest<Result<bool>>;