#region

using MediatR;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetAllGoalsByUserIdQuery(Guid UserId)
    : IRequest<Result<GetAllGoalsByUserIdQueryResponse>>;

public record GetAllGoalsByUserIdQueryResponse(List<GetAllGoalsByUserIdQueryResponseGoals> Goals);

public record GetAllGoalsByUserIdQueryResponseGoals(
    Guid GoalId,
    string Title,
    string? Description,
    GoalPriority? GoalPriority,
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    GoalStatus Status,
    double Progress,
    List<GetAllGoalsByUserIdQueryResponseGoals> Goals);