#region

using MediatR;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public sealed record CreateTaskCommand(
    Guid UserId,
    string Title,
    string? Description,
    DateOnly Date,
    TimeOnly? DefaultTime,
    TaskPriority? Priority,
    TaskType Type,
    RepeatPatternDto? RepeatPattern,
    Guid? ParentGoalId
) : IRequest<Result<CreateTaskCommandResponse>>;

public sealed record CreateTaskCommandResponse(Guid Id);