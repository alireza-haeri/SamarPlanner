using SamarPlanner.Shared.Contracts.Enums;

namespace SamarPlanner.Goal.Application.Dtos;

public record ShortGoal(Guid GoalId, string Title, GoalPriority?  Priority);