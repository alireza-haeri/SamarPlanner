using MediatR;
using SamarPlanner.Shared.Kernel;
using TaskStatus = SamarPlanner.Task.Core.Enums.TaskStatus;

namespace SamarPlanner.Shared.Contracts.Command;

public record ChangeTaskOccurrenceStatusCommand(Guid TaskId, Guid UserId,DateOnly Date, TaskStatus  Status, int? Score) : IRequest<Result<bool>>;