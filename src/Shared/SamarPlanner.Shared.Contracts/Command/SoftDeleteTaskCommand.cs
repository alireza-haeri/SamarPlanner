#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record SoftDeleteTaskCommand(Guid TaskId, Guid UserId) : IRequest<Result<bool>>;