using SamarPlanner.Task.Core.Enums;

namespace SamarPlanner.Task.Core.Entities;

public class RepeatPattern
{
    public RepeatPatternType Type { get; private set; }
    public DateOnly AnchorDate { get; private set; }
    public int? Interval { get; private set; }
    public List<DayOfWeek>? WeekDays { get; private set; }
    public List<int>? MonthDays { get; private set; }


    public void ChangeAnchorDate(DateOnly newDate) => AnchorDate = newDate;

    public static RepeatPattern Create(RepeatPatternType type, DateOnly anchorDate, int? interval,
        List<DayOfWeek>? weekDays, List<int>? monthDays) =>
        new()
        {
            Type = type,
            AnchorDate = anchorDate,
            Interval =  interval,
            WeekDays =  weekDays,   
            MonthDays =  monthDays
        };
}