using MediatR;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeOccurrenceTimeCommand;

public class ChangeOccurrenceTimeCommandHandler(ITaskRepository taskRepository)
:IRequestHandler<Shared.Contracts.Command.ChangeOccurrenceTimeCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(Shared.Contracts.Command.ChangeOccurrenceTimeCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetWithOccurrencesAsTrackingAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر پپدا نشد.");

        task.ChangeOccurrenceTime(request.Date,request.NewTime);
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت وظیفه مورد نظر رخ داده است.");
        
        return Result<bool>.Success(true);
    }
}