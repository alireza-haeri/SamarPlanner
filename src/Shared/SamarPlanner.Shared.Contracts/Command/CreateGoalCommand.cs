using MediatR;
using SamarPlanner.Goal.Core.Entities;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public sealed record CreateGoalCommand(
    Guid UserId,
    string Title,
    string? Description,
    GoalPriority Priority,
    GoalType GoalType,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    Guid? ParentGoalId)
    : IRequest<Result<CreateGoalCommandResponse>>;

public sealed record CreateGoalCommandResponse(Guid GoalId);
