using MediatR;
using Microsoft.Extensions.Logging;
using SamarPlanner.Goal.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Events;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Goal.Application.UseCases.Events;

public class TaskGoalStatusChangedHandler(
    IGoalRepository goalRepository,
    IMediator mediator,
    ILogger<TaskGoalStatusChangedHandler> logger)
    : INotificationHandler<TaskGoalStatusChangedEvent>
{
    public async System.Threading.Tasks.Task Handle(TaskGoalStatusChangedEvent notification, CancellationToken ct)
    {
        try
        {
            var goal = await goalRepository.GetAsTrackingAsync(notification.GoalId, notification.UserId, ct);
            if (goal == null)
            {
                logger.LogWarning("Goal with id {GoalId} for user {UserId} not found", notification.GoalId,
                    notification.UserId);
                return;
            }

            var result = await mediator.Send(
                new GetGoalProgressQuery(
                    notification.GoalId,
                    notification.UserId,
                    goal.PeriodStart,
                    goal.PeriodEnd),
                ct);

            goal.SetProgress(result.OverallProgress);
            await goalRepository.UpdateAsync(goal, ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logger.LogError(e, e.Message);
        }
    }
}