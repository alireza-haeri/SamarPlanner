#region

using MediatR;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetTaskDetailQuery(Guid TaskId, Guid UserId) : IRequest<Result<GetTaskDetailQueryResult>>;

public record GetTaskDetailQueryResult(
    Guid TaskId,
    string Title,
    string? Description,
    TimeOnly? DefaultTime,
    TaskPriority? Priority,
    TaskType Type,
    RepeatPatternDto? RepeatPattern,
    Guid? ParentGoalId);