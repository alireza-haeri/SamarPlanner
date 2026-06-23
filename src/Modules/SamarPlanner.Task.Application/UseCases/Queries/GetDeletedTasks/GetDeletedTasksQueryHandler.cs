using MediatR;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;

namespace SamarPlanner.Task.Application.UseCases.Queries.GetDeletedTasks;

public class GetDeletedTasksQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetDeletedTasksQuery, Result<GetDeletedTasksQueryResult>>
{
    public async Task<Result<GetDeletedTasksQueryResult>> Handle(GetDeletedTasksQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.GetDeletedTasksAsync(request.UserId, cancellationToken);

        var result =
            new GetDeletedTasksQueryResult(
                tasks
                    .Select(t => new GetDeletedTasksQueryResultTasks(t.Id, t.Title))
                    .ToList());

        return Result<GetDeletedTasksQueryResult>.Success(result);
    }
}