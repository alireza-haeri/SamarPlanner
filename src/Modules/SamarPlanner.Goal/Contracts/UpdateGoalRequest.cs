using SamarPlanner.Goal.Core.Entities;

namespace SamarPlanner.Goal.Contracts;

public record UpdateGoalRequest(
    Guid GoalId,
    string Title,
    string? Description,
    GoalPriority Priority,
    GoalType GoalType,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    Guid? ParentGoalId
);