#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record RolloverGoalCommand(Guid UserId, Guid GoalId, DateOnly PeriodStart, DateOnly PeriodEnd)
    : IRequest<Result<RolloverGoalCommandResult>>;

public record RolloverGoalCommandResult(Guid GoalId);