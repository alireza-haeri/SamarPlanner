using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.RestoreOccurrenceTask;

public class RestoreTaskOccurrenceCommandHandler
(ITaskRepository taskRepository)
:IRequestHandler<RestoreTaskOccurrenceCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(RestoreTaskOccurrenceCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetWithOccurrencesAsTrackingWithOutFilterAsync(request.TaskId,request.UserId ,cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر یافت نشد.");

        task.RestoreOccurrence(request.Date);
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت وظیفه مورد نظر رخ داده است.");
        
        return Result<bool>.Success(true);
    }
}