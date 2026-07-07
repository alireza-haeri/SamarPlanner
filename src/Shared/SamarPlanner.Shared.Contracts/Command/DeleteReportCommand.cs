#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record DeleteReportCommand(Guid ReportId, Guid UserId) : IRequest<Result<bool>>;