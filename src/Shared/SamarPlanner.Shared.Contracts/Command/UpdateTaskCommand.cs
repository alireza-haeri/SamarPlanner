using MediatR;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Core.Dtos;
using SamarPlanner.Task.Core.Entities;
using SamarPlanner.Task.Core.Enums;

namespace SamarPlanner.Shared.Contracts.Command;

public record UpdateTaskCommand(
    Guid TaskId,
    Guid UserId,
    string Title,
    string? Description,
    DateOnly Date,
    TimeOnly? DefaultTime,
    TaskPriority? Priority,
    TaskType Type,
    RepeatPatternDto? RepeatPattern,
    Guid? ParentGoalId
) : IRequest<Result<bool>>;