using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.DeleteOccurrenceTask;

public class DeleteTaskOccurrenceCommandHandler(ITaskRepository taskRepository)
    : IRequestHandler<DeleteTaskOccurrenceCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteTaskOccurrenceCommand request, CancellationToken cancellationToken)
    {
        var deleteResult =
            await taskRepository.DeleteOccurrencesWithOutFilterAsync(request.TaskId, request.UserId, request.Date,
                cancellationToken);
        if(!deleteResult)
            return Result<bool>.GeneralFailure("خطا در حذف وظیفه اتفاق افتاده است.");

        return Result<bool>.Success(true);
    }
}