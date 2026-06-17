namespace SamarPlanner.Task.Contracts;

public record ChangeOccurrenceDateRequest(DateOnly OldDate, DateOnly NewDate);