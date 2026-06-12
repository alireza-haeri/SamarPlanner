using MediatR;
using SamarPlanner.Goal.Core.Entities;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record UpdateGoalCommand(
    Guid GoalId,
    Guid UserId,
    string Title,
    string? Description,
    GoalPriority Priority,  
    GoalType GoalType,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    Guid? ParentGoalId)
    : IRequest<Result<bool>>;