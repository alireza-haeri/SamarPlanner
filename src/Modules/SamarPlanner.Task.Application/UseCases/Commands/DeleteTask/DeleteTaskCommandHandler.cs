using MediatR;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Commands.DeleteTask;

public class DeleteTaskCommandHandler
(ITaskRepository taskRepository)
:IRequestHandler<DeleteTaskCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var deleteResult = await taskRepository.DeleteTaskWithOccurrencesWithOutFilterAsync(request.TaskId, request.UserId, cancellationToken);
        if (!deleteResult)
            return Result<bool>.NotfoundFailure("خطا در حذف وظیله مورد نظر اتفاق افتاده است.");
        
        return Result<bool>.Success(true);
    }
}