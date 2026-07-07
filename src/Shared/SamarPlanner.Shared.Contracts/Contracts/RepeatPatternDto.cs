using SamarPlanner.Shared.Contracts.Enums;

namespace SamarPlanner.Shared.Contracts.Contracts;

public record RepeatPatternDto(
    RepeatPatternType Type,
    DateOnly AnchorDate,
    int? Interval,
    List<DayOfWeek>? WeekDays,
    List<int>? MonthDays);