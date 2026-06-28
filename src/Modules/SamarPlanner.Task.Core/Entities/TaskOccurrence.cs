using TaskStatus = SamarPlanner.Task.Core.Enums.TaskStatus;

namespace SamarPlanner.Task.Core.Entities;

public class TaskOccurrence
{
    public const string TableName = "TaskOccurrence";
    public Guid Id { get; private set; }
    public Guid TaskId { get; private
        set; }
    public DateOnly Date { get; private set; }
    public TimeOnly? Time { get; private set; }
    public TaskStatus Status { get; private set; }
    public int? Score { get; private set; }
    public bool IsSkipped { get; private set; }
    public Task Task { get; set; } = null!;

    public static TaskOccurrence Create(Guid taskId, DateOnly date, TimeOnly? time) => new()
    {
        TaskId = taskId,
        Date = date,
        Time = time,
        Status = TaskStatus.Pending,
        Score = null,
        IsSkipped = false,
    };

    public void ChangeStatus(TaskStatus status, int? score)
    {

        if(Date > DateOnly.FromDateTime(DateTime.Now))
            throw new InvalidOperationException("Cannot update status of a future occurrence.");
      
        Status = status;
        Score = status switch
        {
            TaskStatus.Done => 10,
            TaskStatus.NotDone => 0,
            TaskStatus.Pending => null,
            TaskStatus.AlmostDone => score,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }

    public void ChangeTime(TimeOnly? time) => Time = time;

    public void ChangeSkipped(bool isSkipped) => IsSkipped = isSkipped;

    public void ChangeDate(DateOnly newDate) => Date = newDate;
}