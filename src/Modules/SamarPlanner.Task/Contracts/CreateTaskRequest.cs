using SamarPlanner.Task.Core.Dtos;
using SamarPlanner.Task.Core.Enums;

namespace SamarPlanner.Task.Contracts;

public record CreateTaskRequest(
    string Title,
    string? Description,
    DateOnly Date,
    TimeOnly? DefaultTime,
    TaskPriority? Priority,
    TaskType Type,
    RepeatPatternDto? RepeatPattern,
    Guid? ParentGoalId
);