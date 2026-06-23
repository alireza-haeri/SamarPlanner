using MediatR;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Core.Dtos;
using SamarPlanner.Task.Core.Enums;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetTaskDetailQuery(Guid TaskId, Guid UserId): IRequest<Result<GetTaskDetailQueryResult>>;

public record GetTaskDetailQueryResult(
    Guid TaskId,
    string Title,
    string? Description,
    TimeOnly? DefaultTime,
    TaskPriority? Priority,
    SamarPlanner.Task.Core.Enums.TaskType Type,
    RepeatPatternDto? RepeatPattern,
    Guid? ParentGoalId);