using MediatR;
using SamarPlanner.Goal.Core.Entities;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetAllGoalsByUserIdQuery(Guid UserId) 
    : IRequest<Result<GetAllGoalsByUserIdQueryResponse>>;

public record GetAllGoalsByUserIdQueryResponse(List<GetAllGoalsByUserIdQueryResponseGoals> Goals);

public record GetAllGoalsByUserIdQueryResponseGoals(
    Guid GoalId,
    string Title,
    string? Description,
    GoalType GoalType,
    GoalPriority? GoalPriority,
    List<GetAllGoalsByUserIdQueryResponseGoals> Goals);