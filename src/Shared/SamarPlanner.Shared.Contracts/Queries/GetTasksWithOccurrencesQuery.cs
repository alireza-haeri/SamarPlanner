using MediatR;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Core.Enums;
using TaskStatus = SamarPlanner.Task.Core.Enums.TaskStatus;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetTasksWithOccurrencesQuery(Guid UserId, DateOnly From, DateOnly To)
    : IRequest<Result<GetTasksWithOccurrencesQueryResult>>;

public record GetTasksWithOccurrencesQueryResult(List<GetTasksWithOccurrencesQueryResultTasks> Tasks);

public record GetTasksWithOccurrencesQueryResultTasks(
    Guid TaskId,
    string Title,
    TaskPriority? Priority,
    TaskType Type,
    Guid? ParentGoalId,
    List<GetTasksWithOccurrencesQueryResultTaskOccurrences> Occurrences);

public record GetTasksWithOccurrencesQueryResultTaskOccurrences(
    DateOnly Date,
    TimeOnly? Time,
    TaskStatus Status,
    int? Score,
    bool IsSkipped);