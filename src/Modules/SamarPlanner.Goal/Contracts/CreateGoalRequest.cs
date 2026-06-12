using SamarPlanner.Goal.Core.Entities;

namespace SamarPlanner.Goal.Contracts;

public sealed record CreateGoalRequest(
    string Title,
    string? Description,
    GoalPriority Priority,
    GoalType GoalType,
    DateTime PeriodStart,
    DateTime PeriodEnd,
    Guid? ParentGoalId);