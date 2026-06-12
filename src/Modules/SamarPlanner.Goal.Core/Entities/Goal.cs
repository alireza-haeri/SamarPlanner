namespace SamarPlanner.Goal.Core.Entities;

public class Goal
{
    public const string TableName = "Goal";

    public Guid Id { get; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public GoalPriority? GoalPriority { get; private set; }
    public GoalType GoalType { get; private set; }
    public Guid? ParentGoalId { get; private set; }
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public Guid UserId { get; private set; }

    public static Goal Create(Guid userId, string title, string? description, GoalType goalType, GoalPriority priority,
        DateTime periodStart, DateTime periodEnd, Guid? prentGoalId) =>
        new()
        {
            UserId = userId,
            Title = title,
            Description = description,
            GoalType = goalType,
            GoalPriority = priority,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            ParentGoalId = prentGoalId
        };

    public void Update(string title, string? description, GoalType goalType, GoalPriority priority,
        DateTime periodStart, DateTime periodEnd, Guid? parentGoalId)
    {
        Title = title;
        Description = description;
        GoalType = goalType;
        GoalPriority = priority;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        ParentGoalId = parentGoalId;
    }
}

public enum GoalPriority
{
    Low = 1,
    Medium = 2,
    High = 3
}

public enum GoalType
{
    Long = 1,
    Medium = 2,
    Short = 3,
}