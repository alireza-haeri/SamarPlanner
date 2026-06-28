using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Events;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeTaskOccurrenceStatus;

public class ChangeTaskOccurrenceStatusCommandHandler(ITaskRepository taskRepository, IMediator mediator)
    : IRequestHandler<ChangeTaskOccurrenceStatusCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ChangeTaskOccurrenceStatusCommand request,
        CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetWithOccurrencesAsTrackingAsync(request.TaskId, request.UserId,
            cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر یافت نشد.");

        task.ChangeOccurrenceStatus(request.Date, request.Status, request.Score);

        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت وظیفه مورد نظر رخ داده است.");
        
        if (task.ParentGoalId.HasValue)
            await mediator.Publish(new TaskGoalStatusChangedEvent(request.TaskId, request.UserId,
                task.ParentGoalId.Value), cancellationToken);

        return Result<bool>.Success(true);
    }
}