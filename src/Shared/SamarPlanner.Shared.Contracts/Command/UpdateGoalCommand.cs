#region

using MediatR;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record UpdateGoalCommand(
    Guid GoalId,
    Guid UserId,
    string Title,
    string? Description,
    GoalPriority? Priority,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    Guid? ParentGoalId)
    : IRequest<Result<bool>>;