using System.Globalization;
using MediatR;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;
using SamarPlanner.Task.Core.Entities;
using TaskStatus = SamarPlanner.Shared.Contracts.Enums.TaskStatus;

namespace SamarPlanner.Task.Application.UseCases.Queries.GetTasksWithOccurrences;

public class GetTasksWithOccurrencesQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetTasksWithOccurrencesQuery, Result<GetTasksWithOccurrencesQueryResult>>
{
    public async Task<Result<GetTasksWithOccurrencesQueryResult>> Handle(GetTasksWithOccurrencesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new GetTasksWithOccurrencesQueryResult([]);

        var tasks = await taskRepository.GetWithOccurrencesAndRepeatPatternAsync(
            request.UserId,
            request.From,
            request.To,
            cancellationToken);
        if (!tasks.Any())
            return Result<GetTasksWithOccurrencesQueryResult>.Success(result);

        foreach (var task in tasks)
        {
            List<GetTasksWithOccurrencesQueryResultTaskOccurrences> occurrences;

            if (task.RepeatPattern is null)
            {
                occurrences = task.Occurrences
                    .Select(o => new GetTasksWithOccurrencesQueryResultTaskOccurrences(
                        o.Date, o.Time ?? task.DefaultTime, o.Status, o.Score, o.IsSkipped))
                    .ToList();

                if (!occurrences.Any())
                    continue;
            }
            else
            {
                occurrences = [];
                var from = task.RepeatPattern.AnchorDate > request.From
                    ? task.RepeatPattern.AnchorDate
                    : request.From;

                for (var day = from; day <= request.To; day = day.AddDays(1))
                {
                    if (!IsScheduledFor(task.RepeatPattern, day))
                        continue;
                    
                    var saved = task.Occurrences.FirstOrDefault(o => o.Date == day);

                    occurrences.Add(saved is not null
                        ? new GetTasksWithOccurrencesQueryResultTaskOccurrences(
                            saved.Date, saved.Time ?? task.DefaultTime, saved.Status, saved.Score, saved.IsSkipped)
                        : new GetTasksWithOccurrencesQueryResultTaskOccurrences(
                            day, task.DefaultTime, TaskStatus.Pending, null, false));
                }
            }

            if (!occurrences.Any())
                continue;

            result.Tasks.Add(new GetTasksWithOccurrencesQueryResultTasks(
                task.Id, task.Title, task.Priority, task.Type, task.ParentGoalId, occurrences));
        }

        return Result<GetTasksWithOccurrencesQueryResult>.Success(result);
    }

    private static bool IsScheduledFor(RepeatPattern pattern, DateOnly date)
    {
        return pattern.Type switch
        {
            RepeatPatternType.Daily => IsDailyMatch(pattern, date),
            RepeatPatternType.WeeklyOnDays => IsWeeklyMatch(pattern, date),
            RepeatPatternType.MonthlyOnDays => IsMonthlyMatch(pattern, date),
            _ => false
        };
    }

    private static bool IsDailyMatch(RepeatPattern pattern, DateOnly date)
    {
        var interval = pattern.Interval ?? 1;
        return (date.DayNumber - pattern.AnchorDate.DayNumber) % interval == 0;
    }

    private static bool IsWeeklyMatch(RepeatPattern pattern, DateOnly date)
    {
        if (!pattern.WeekDays!.Contains(date.DayOfWeek))
            return false;

        var interval = pattern.Interval ?? 1;
        if (interval == 1)
            return true;

        var anchorWeek = ISOWeek.GetWeekOfYear(pattern.AnchorDate.ToDateTime(TimeOnly.MinValue));
        var anchorYear = ISOWeek.GetYear(pattern.AnchorDate.ToDateTime(TimeOnly.MinValue));

        var dateWeek = ISOWeek.GetWeekOfYear(date.ToDateTime(TimeOnly.MinValue));
        var dateYear = ISOWeek.GetYear(date.ToDateTime(TimeOnly.MinValue));

        var totalWeeks = (dateYear - anchorYear) * 52 + (dateWeek - anchorWeek);

        return totalWeeks >= 0 && totalWeeks % interval == 0;
    }

    private static bool IsMonthlyMatch(RepeatPattern pattern, DateOnly date)
    {
        if (!pattern.MonthDays!.Contains(date.Day))
            return false;

        var interval = pattern.Interval ?? 1;
        if (interval == 1)
            return true;

        var monthsSinceAnchor = (date.Year - pattern.AnchorDate.Year) * 12
                                + (date.Month - pattern.AnchorDate.Month);
        return monthsSinceAnchor % interval == 0;
    }
}