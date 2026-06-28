using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record RolloverGoalCommand(Guid UserId, Guid GoalId, DateOnly PeriodStart, DateOnly PeriodEnd)
    : IRequest<Result<RolloverGoalCommandResult>>;

public record RolloverGoalCommandResult(Guid GoalId);