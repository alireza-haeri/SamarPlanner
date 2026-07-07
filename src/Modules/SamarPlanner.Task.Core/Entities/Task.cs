using SamarPlanner.Shared.Contracts.Enums;
using TaskStatus = SamarPlanner.Shared.Contracts.Enums.TaskStatus;

namespace SamarPlanner.Task.Core.Entities;

public class Task
{
    public const string TableName = "Task";
    public Guid Id { get; private init; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public TaskPriority? Priority { get; private set; }
    public TaskType Type { get; private set; }
    public Guid? ParentGoalId { get; private set; }
    public TimeOnly? DefaultTime { get; private set; }
    public bool SoftDeleted { get; private set; }
    public RepeatPattern? RepeatPattern { get; private set; }
    private readonly List<TaskOccurrence> _occurrences = new();
    public IReadOnlyCollection<TaskOccurrence> Occurrences => _occurrences;

    public static Task Create(
        Guid userId,
        string title,
        string? description,
        TaskPriority? priority,
        TaskType type,
        Guid? parentGoalId,
        TimeOnly? defaultTime,
        RepeatPattern? repeatPattern,
        DateOnly? date)
    {
        var task = new Task
        {
            UserId = userId,
            Title = title,
            Description = description,
            Priority = priority,
            Type = type,
            ParentGoalId = parentGoalId,
            DefaultTime = defaultTime,
            RepeatPattern = repeatPattern,
            SoftDeleted = false
        };

        if (repeatPattern is null)
        {
            if (date is null)
                throw new ArgumentException("Non-repeating task requires a date.");

            task._occurrences.Add(TaskOccurrence.Create(task.Id, date.Value, null));
        }

        return task;
    }

    public void Update(
        string title,
        string? description,
        TaskPriority? priority,
        TaskType type,
        Guid? parentGoalId,
        TimeOnly? defaultTime,
        RepeatPattern? repeatPattern)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Type = type;
        ParentGoalId = parentGoalId;
        DefaultTime = defaultTime;
        RepeatPattern = repeatPattern;
    }

    public void SoftDelete() => SoftDeleted = true;
    public void Restore() => SoftDeleted = false;

    public TaskOccurrence GetOrCreateOccurrence(DateOnly date)
    {
        var existing = _occurrences.FirstOrDefault(o => o.Date == date);
        if (existing is not null)
            return existing;

        // if (RepeatPattern is null)
        //     throw new InvalidOperationException("Non-repeating task must already have its single occurrence.");

        var occurrence = TaskOccurrence.Create(Id, date, null);
        _occurrences.Add(occurrence);
        return occurrence;
    }

    public void ChangeOccurrenceDate(DateOnly oldDate, DateOnly newDate)
    {
        if (RepeatPattern is not null)
            RepeatPattern.ChangeAnchorDate(newDate);

        var occurence = GetOrCreateOccurrence(oldDate);

        occurence.ChangeDate(newDate);
    }

    public void ChangeOccurrenceTime(DateOnly date, TimeOnly newTime)
    {
        var occurrence = GetOrCreateOccurrence(date);
        occurrence.ChangeTime(newTime);
    }

    public void ChangeOccurrenceSkipped(DateOnly date, bool isSkipped)
    {
        var occurrence = GetOrCreateOccurrence(date);
        occurrence.ChangeSkipped(isSkipped);
    }

    public void ChangeOccurrenceStatus(DateOnly date, TaskStatus status, int? score)
    {
        var occurrence = GetOrCreateOccurrence(date);
        occurrence.ChangeStatus(status, score);
    }
}