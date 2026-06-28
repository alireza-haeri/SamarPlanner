using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Events;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.UpdateTask;

public class UpdateTaskCommandHandler(ITaskRepository taskRepository,IMediator mediator)
    : IRequestHandler<UpdateTaskCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetWithRepeatPatternAsTrackingAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر یافت نشد.");

        task.Update(
            title: request.Title,
            description: request.Description,
            defaultTime: request.DefaultTime,
            priority: request.Priority,
            type: request.Type,
            repeatPattern: request.RepeatPattern?.ToRepeatPattern(),
            parentGoalId: request.ParentGoalId);
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if(!updateResult)
            return Result<bool>.GeneralFailure("خطا در بروزرسانی وظیفه اتفاق افتاده است.");
        
        if (task.ParentGoalId.HasValue)
            await mediator.Publish(new TaskGoalStatusChangedEvent(request.TaskId, request.UserId,
                task.ParentGoalId.Value), cancellationToken);
        
        return Result<bool>.Success(true);
    }
}