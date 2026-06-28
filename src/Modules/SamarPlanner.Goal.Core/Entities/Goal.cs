using SamarPlanner.Goal.Core.Enums;

namespace SamarPlanner.Goal.Core.Entities;

public class Goal
{
    public const string TableName = "Goal";

    public Guid Id { get; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public GoalPriority? GoalPriority { get; private set; }
    public GoalStatus Status { get; set; }
    public Guid? ParentGoalId { get; private set; }
    public Guid? RolledOverId { get; private set; }
    public DateOnly PeriodStart { get; private set; }
    public DateOnly PeriodEnd { get; private set; }
    public double Progress { get; private set; }
    public Guid UserId { get; private set; }

    public static Goal Create(Guid userId, string title, string? description, GoalPriority? priority,
        DateOnly periodStart, DateOnly periodEnd, Guid? prentGoalId) =>
        new()
        {
            UserId = userId,
            Title = title,
            Description = description,
            GoalPriority = priority,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            ParentGoalId = prentGoalId
        };

    public void Update(string title, string? description, GoalPriority? priority,
        DateOnly periodStart, DateOnly periodEnd, Guid? parentGoalId)
    {
        Title = title;
        Description = description;
        GoalPriority = priority;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        ParentGoalId = parentGoalId;
    }

    public void Achieve()
    {
        Status = GoalStatus.Achieved;
    }

    public void Fail()
    {
        Status = GoalStatus.Failed;
    }

    public void Active()
    {
        Status = GoalStatus.Active;
    }

    public Goal Rollover(DateOnly periodStart, DateOnly periodEnd)
    {
        Status = GoalStatus.RolledOver;

        return new Goal
        {
            UserId = UserId,
            Title = Title,
            Description = Description,
            GoalPriority = GoalPriority,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            ParentGoalId = ParentGoalId,
            RolledOverId = Id,
            Status = GoalStatus.Active
        };
    }

    public void SetProgress(double overallProgress)
    {
        Progress = overallProgress;
    }
}