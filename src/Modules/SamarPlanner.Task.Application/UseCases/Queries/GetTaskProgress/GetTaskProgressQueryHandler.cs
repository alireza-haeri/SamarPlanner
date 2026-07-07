using MediatR;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Task.Application.Abstractions;
using SamarPlanner.Task.Core.Entities;
using TaskStatus = SamarPlanner.Shared.Contracts.Enums.TaskStatus;

namespace SamarPlanner.Task.Application.UseCases.Queries.GetTaskProgress;

public class GetGoalProgressQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetGoalProgressQuery, GoalProgressResult>
{
    public async Task<GoalProgressResult> Handle(
        GetGoalProgressQuery request, CancellationToken ct)
    {
        var occurrences = await taskRepository
            .GetOccurrencesByGoalIdUntilDateAsync(
                request.GoalId, 
                request.UserId,
                request.PeriodStart,
                request.PeriodEnd,
                ct);

        if (!occurrences.Any())
            return new GoalProgressResult (0) ;

        var totalScore = occurrences.Sum(CalculateScore);
        var maxScore = occurrences.Count * 10;

        return new GoalProgressResult((double)totalScore / maxScore * 100);
    }

    private int CalculateScore(TaskOccurrence o) => o.Status switch
    {
        TaskStatus.Pending     => 0,
        TaskStatus.NotDone     => o.Score ?? 0,
        TaskStatus.AlmostDone  => o.Score ?? 5,
        TaskStatus.Done        => o.Score ?? 10,
        _ => 0
    };
}