using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Events;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.SoftDeleteTask;

public class SoftDeleteTaskCommandHandler
(ITaskRepository taskRepository , IMediator mediator)
:IRequestHandler<SoftDeleteTaskCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(SoftDeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetAsTrackingAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر یافت نشد.");
        
        task.SoftDelete();
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if(!updateResult)
            return Result<bool>.GeneralFailure("خطا در حذف وظیفه اتفاق افتاده است.");
        
        if (task.ParentGoalId.HasValue)
            await mediator.Publish(new TaskGoalStatusChangedEvent(request.TaskId, request.UserId,
                task.ParentGoalId.Value), cancellationToken);

        return Result<bool>.Success(true);
    }
}