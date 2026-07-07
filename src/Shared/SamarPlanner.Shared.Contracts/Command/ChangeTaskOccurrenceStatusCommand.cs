#region

using MediatR;
using SamarPlanner.Shared.Kernel;
using TaskStatus = SamarPlanner.Shared.Contracts.Enums.TaskStatus;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record ChangeTaskOccurrenceStatusCommand(Guid TaskId, Guid UserId, DateOnly Date, TaskStatus Status, int? Score)
    : IRequest<Result<bool>>;