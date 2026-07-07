using TaskStatus = SamarPlanner.Shared.Contracts.Enums.TaskStatus;

namespace SamarPlanner.Task.Contracts;

public record ChangeOccurrenceStatusRequest( DateOnly Date, TaskStatus Status, int? Score);