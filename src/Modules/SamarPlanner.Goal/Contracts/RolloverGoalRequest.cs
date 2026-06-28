namespace SamarPlanner.Goal.Contracts;

public record RolloverGoalRequest(DateOnly PeriodStart, DateOnly PeriodEnd);