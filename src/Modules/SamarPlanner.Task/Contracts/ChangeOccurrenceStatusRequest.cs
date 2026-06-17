using TaskStatus = SamarPlanner.Task.Core.Enums.TaskStatus;

namespace SamarPlanner.Task.Contracts;

public record ChangeOccurrenceStatusRequest( DateOnly Date, TaskStatus Status, int? Score);