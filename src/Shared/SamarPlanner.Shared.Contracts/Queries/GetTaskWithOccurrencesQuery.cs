using MediatR;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Core.Enums;
using TaskStatus = SamarPlanner.Task.Core.Enums.TaskStatus;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetTaskWithOccurrencesQuery(Guid UserId, DateOnly From, DateOnly To)
    : IRequest<Result<GetTaskWithOccurrencesQueryResult>>;

public record GetTaskWithOccurrencesQueryResult(List<GetTaskWithOccurrencesQueryResultTasks> Tasks);

public record GetTaskWithOccurrencesQueryResultTasks(
    Guid TaskId,
    string Title,
    TaskPriority? Priority,
    TaskType Type,
    Guid? ParentGoalId,
    List<GetTaskWithOccurrencesQueryResultTaskOccurrences> Occurrences);

public record GetTaskWithOccurrencesQueryResultTaskOccurrences(
    DateOnly Date,
    TimeOnly? Time,
    TaskStatus Status,
    int? Score,
    bool IsSkipped);