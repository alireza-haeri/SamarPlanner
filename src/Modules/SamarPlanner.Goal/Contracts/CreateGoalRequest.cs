using SamarPlanner.Goal.Core.Entities;
using SamarPlanner.Goal.Core.Enums;

namespace SamarPlanner.Goal.Contracts;

public sealed record CreateGoalRequest(
    string Title,
    string? Description,
    GoalPriority? Priority,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    Guid? ParentGoalId);