using MediatR;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Queries.GetTaskDetail;

public class GetTaskDetailQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetTaskDetailQuery, Result<GetTaskDetailQueryResult>>
{
    public async Task<Result<GetTaskDetailQueryResult>> Handle(GetTaskDetailQuery request,
        CancellationToken cancellationToken)
    {
        
        var task = await taskRepository.GetWithRepeatPatternAsTrackingAsync(request.TaskId, request.UserId, cancellationToken);
        if (task is null)
            return Result<GetTaskDetailQueryResult>.NotfoundFailure("وظیفه مورد نظر یافت نشد.");

        var result = new GetTaskDetailQueryResult(
            TaskId: task.Id,
            Title: task.Title,
            Description: task.Description,
            DefaultTime: task.DefaultTime,
            Priority: task.Priority,
            Type: task.Type,
            RepeatPattern: task.RepeatPattern?.ToDto(),
            ParentGoalId: task.ParentGoalId
        );
        
        return Result<GetTaskDetailQueryResult>.Success(result);
    }
}