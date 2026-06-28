using MediatR;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetGoalProgressQuery(
    Guid GoalId,
    Guid UserId,
    DateOnly PeriodStart,
    DateOnly PeriodEnd
) : IRequest<GoalProgressResult>;

public record GoalProgressResult(double OverallProgress);