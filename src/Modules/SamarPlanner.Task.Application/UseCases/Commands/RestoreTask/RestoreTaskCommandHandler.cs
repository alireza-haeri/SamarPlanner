using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.RestoreTask;

public class RestoreSoftDeleteTaskCommandHandler
(ITaskRepository taskRepository)
:IRequestHandler<RestoreTaskCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(RestoreTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetAsTrackingWithOutFilterAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر یافت نشد.");
        
        task.Restore();
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if(!updateResult)
            return Result<bool>.GeneralFailure("خطا در بازیابی وظیفه اتفاق افتاده است.");

        return Result<bool>.Success(true);
    }
}