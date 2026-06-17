using MediatR;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeOccurrenceDateCommand;

public class ChangeOccurrenceDateCommandHandler(ITaskRepository taskRepository)
:IRequestHandler<Shared.Contracts.Command.ChangeOccurrenceDateCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(Shared.Contracts.Command.ChangeOccurrenceDateCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetWithOccurrencesAndRepeatPatternAsTrackingAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر پپدا نشد.");

        task.ChangeOccurrenceDate(request.OldDate, request.NewDate);
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت وظیفه مورد نظر رخ داده است.");
        
        return Result<bool>.Success(true);
    }
}