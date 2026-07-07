using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Contracts.Enums;

namespace SamarPlanner.Task.Contracts;

public record UpdateTaskRequest(
    string Title,
    string? Description,
    DateOnly Date,
    TimeOnly? DefaultTime,
    TaskPriority? Priority,
    TaskType Type,
    RepeatPatternDto? RepeatPattern,
    Guid? ParentGoalId
);