using MediatR;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Goal.Application.UseCases.Queries.GetAllShortGoalsByUserId;

public class GetAllShortGoalsByUserIdQueryHandler(
    IGoalRepository goalRepository)
    : IRequestHandler<GetAllShortGoalsByUserIdQuery, Result<GetAllShortGoalsByUserIdQueryResponse>>
{
    public async Task<Result<GetAllShortGoalsByUserIdQueryResponse>> Handle(
        GetAllShortGoalsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var goals = await goalRepository.GetAllShortAsync(request.UserId, cancellationToken);
        if (!goals.Any())
            return Result<GetAllShortGoalsByUserIdQueryResponse>.Success(
                new GetAllShortGoalsByUserIdQueryResponse([]));

        return Result<GetAllShortGoalsByUserIdQueryResponse>.Success(
            new GetAllShortGoalsByUserIdQueryResponse(goals.Select(g =>
                new GetAllShortGoalsByUserIdQueryResponseGoals(
                    g.GoalId, g.Title, g.Priority)
            ).ToList()));
    }
}