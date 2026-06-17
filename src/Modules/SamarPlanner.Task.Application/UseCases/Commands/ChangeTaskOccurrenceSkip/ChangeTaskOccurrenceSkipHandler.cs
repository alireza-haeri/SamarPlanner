using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeTaskOccurrenceSkip;

public class ChangeTaskOccurrenceSkipHandler(ITaskRepository taskRepository)
    : IRequestHandler<ChangeTaskOccurrenceSkipCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ChangeTaskOccurrenceSkipCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetWithOccurrencesAsTrackingAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<bool>.NotfoundFailure("وظیفه مورد نظر پپدا نشد.");

        task.ChangeOccurrenceSkipped(request.Date, request.IsSkipped);
        
        var updateResult = await taskRepository.UpdateAsync(task, cancellationToken);
        if (!updateResult)
            return Result<bool>.GeneralFailure("خطایی در ویرایش وضعیت وظیفه مورد نظر رخ داده است.");
        
        return Result<bool>.Success(true);
    }
}