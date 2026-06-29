using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record DeleteReportCommand(Guid  ReportId, Guid UserId) : IRequest<Result<bool>>;