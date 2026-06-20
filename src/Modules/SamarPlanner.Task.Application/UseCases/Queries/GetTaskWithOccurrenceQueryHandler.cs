using System.Globalization;
using MediatR;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Application.Abstractions;
using SamarPlanner.Task.Core.Entities;
using SamarPlanner.Task.Core.Enums;
using TaskStatus = SamarPlanner.Task.Core.Enums.TaskStatus;

namespace SamarPlanner.Task.Application.UseCases.Queries;

public class GetTaskWithOccurrenceQueryHandler(ITaskRepository taskRepository)
    : IRequestHandler<GetTaskWithOccurrencesQuery, Result<GetTaskWithOccurrencesQueryResult>>
{
    public async Task<Result<GetTaskWithOccurrencesQueryResult>> Handle(GetTaskWithOccurrencesQuery request,
        CancellationToken cancellationToken)
    {
        var result = new GetTaskWithOccurrencesQueryResult([]);

        var tasks = await taskRepository.GetWithOccurrencesAndRepeatPatternAsync(
            request.UserId,
            request.From,
            request.To,
            cancellationToken);
        if (!tasks.Any())
            return Result<GetTaskWithOccurrencesQueryResult>.Success(result);

        foreach (var task in tasks)
        {
            List<GetTaskWithOccurrencesQueryResultTaskOccurrences> occurrences;

            if (task.RepeatPattern is null)
            {
                occurrences = task.Occurrences
                    .Select(o => new GetTaskWithOccurrencesQueryResultTaskOccurrences(
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
                        ? new GetTaskWithOccurrencesQueryResultTaskOccurrences(
                            saved.Date, saved.Time ?? task.DefaultTime, saved.Status, saved.Score, saved.IsSkipped)
                        : new GetTaskWithOccurrencesQueryResultTaskOccurrences(
                            day, task.DefaultTime, TaskStatus.Pending, null, false));
                }
            }

            if (!occurrences.Any())
                continue;

            result.Tasks.Add(new GetTaskWithOccurrencesQueryResultTasks(
                task.Id, task.Title, task.Priority, task.Type, task.ParentGoalId, occurrences));
        }

        return Result<GetTaskWithOccurrencesQueryResult>.Success(result);
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