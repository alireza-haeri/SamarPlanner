namespace SamarPlanner.Task.Contracts;

public record ChangeOccurrenceTimeRequest( DateOnly Date, TimeOnly NewTime);