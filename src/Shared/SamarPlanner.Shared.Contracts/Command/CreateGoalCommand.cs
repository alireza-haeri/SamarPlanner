#region

using MediatR;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public sealed record CreateGoalCommand(
    Guid UserId,
    string Title,
    string? Description,
    GoalPriority? Priority,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    Guid? ParentGoalId)
    : IRequest<Result<CreateGoalCommandResponse>>;

public sealed record CreateGoalCommandResponse(Guid GoalId);