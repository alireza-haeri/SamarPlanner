using SamarPlanner.Task.Core.Entities;
using SamarPlanner.Task.Core.Enums;

namespace SamarPlanner.Task.Core.Dtos;

public record RepeatPatternDto(
    RepeatPatternType Type,
    DateOnly AnchorDate,
    int? Interval,
    List<DayOfWeek>? WeekDays,
    List<int>? MonthDays)
{
    public RepeatPattern ToRepeatPattern() =>
        RepeatPattern.Create(Type, AnchorDate, Interval, WeekDays, MonthDays);
}