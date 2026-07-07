#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public sealed record GetDeletedTasksQuery(Guid UserId) : IRequest<Result<GetDeletedTasksQueryResult>>;

public sealed record GetDeletedTasksQueryResult(List<GetDeletedTasksQueryResultTasks> Tasks);

public sealed record GetDeletedTasksQueryResultTasks(
    Guid TaskId,
    string Title);