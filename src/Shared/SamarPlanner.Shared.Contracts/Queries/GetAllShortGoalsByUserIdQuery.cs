using MediatR;
using SamarPlanner.Goal.Core.Enums;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetAllShortGoalsByUserIdQuery(Guid  UserId) : IRequest<Result<GetAllShortGoalsByUserIdQueryResponse>>;

public record GetAllShortGoalsByUserIdQueryResponse(List<GetAllShortGoalsByUserIdQueryResponseGoals> Goals);
public record GetAllShortGoalsByUserIdQueryResponseGoals(Guid GoalId, string Title,GoalPriority? Priority);