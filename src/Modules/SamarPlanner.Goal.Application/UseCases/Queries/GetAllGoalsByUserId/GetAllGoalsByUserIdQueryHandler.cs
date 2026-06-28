using FluentValidation;
using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Goal.Core.Entities;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Queries.GetAllGoalsByUserId;

public class GetAllGoalsByUserIdQueryHandler(
    IGoalRepository goalRepository)
    : IRequestHandler<GetAllGoalsByUserIdQuery, Result<GetAllGoalsByUserIdQueryResponse>>
{
    public async Task<Result<GetAllGoalsByUserIdQueryResponse>> Handle(
        GetAllGoalsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var goals = await goalRepository.GetAllAsync(request.UserId, cancellationToken);
        if (!goals.Any())
            return Result<GetAllGoalsByUserIdQueryResponse>.Success(
                new GetAllGoalsByUserIdQueryResponse([]));

        var childrenLookup = goals
            .Where(g => g.ParentGoalId.HasValue)
            .ToLookup(g => g.ParentGoalId!.Value);

        var rootGoals = goals.Where(g => g.ParentGoalId == null).ToList();

        GetAllGoalsByUserIdQueryResponseGoals MapGoal(Core.Entities.Goal goal)
        {
            var children = childrenLookup[goal.Id]
                .Select(MapGoal)
                .ToList();

            return new GetAllGoalsByUserIdQueryResponseGoals(
                GoalId: goal.Id,
                Title: goal.Title,
                Description: goal.Description,
                GoalPriority: goal.GoalPriority,
                PeriodStart: goal.PeriodStart,
                PeriodEnd: goal.PeriodEnd,
                Status: goal.Status,
                Progress: goal.Progress,
                Goals: children
            );
        }

        var resultGoals = rootGoals.Select(MapGoal).ToList();

        return Result<GetAllGoalsByUserIdQueryResponse>.Success(
            new GetAllGoalsByUserIdQueryResponse(resultGoals));
    }
}