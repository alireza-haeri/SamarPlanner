using SamarPlanner.Goal.Core.Entities;
using SamarPlanner.Shared.Contracts.Enums;

namespace SamarPlanner.Goal.Contracts;

public record UpdateGoalRequest(
    Guid GoalId,
    string Title,
    string? Description,
    GoalPriority? Priority,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    Guid? ParentGoalId
);