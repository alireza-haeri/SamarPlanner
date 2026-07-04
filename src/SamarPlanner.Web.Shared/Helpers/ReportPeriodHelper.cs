using System.Globalization;

namespace SamarPlanner.Web.Shared.Helpers;

public enum ReportPeriodView
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public static class ReportPeriodHelper
{
    private static readonly PersianCalendar PersianCalendar = new();

    public static bool TryParse(string? value, out ReportPeriodView period)
    {
        period = ReportPeriodView.Daily;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        return value.Trim().ToLowerInvariant() switch
        {
            "daily" or "day" => SetPeriod(ReportPeriodView.Daily, out period),
            "weekly" or "week" => SetPeriod(ReportPeriodView.Weekly, out period),
            "monthly" or "month" => SetPeriod(ReportPeriodView.Monthly, out period),
            "yearly" or "year" => SetPeriod(ReportPeriodView.Yearly, out period),
            _ => false
        };
    }

    public static ReportPeriodView ParseOrDefault(string? value)
    {
        return TryParse(value, out var period) ? period : ReportPeriodView.Daily;
    }

    public static string ToRouteSegment(this ReportPeriodView period)
    {
        return period switch
        {
            ReportPeriodView.Daily => "daily",
            ReportPeriodView.Weekly => "weekly",
            ReportPeriodView.Monthly => "monthly",
            ReportPeriodView.Yearly => "yearly",
            _ => "daily"
        };
    }

    public static string ToDisplayName(this ReportPeriodView period)
    {
        return period switch
        {
            ReportPeriodView.Daily => "روزانه",
            ReportPeriodView.Weekly => "هفتگی",
            ReportPeriodView.Monthly => "ماهانه",
            ReportPeriodView.Yearly => "سالانه",
            _ => "روزانه"
        };
    }

    public static (DateTimeOffset Start, DateTimeOffset End) GetBounds(ReportPeriodView period, DateTimeOffset reference)
    {
        var dt = reference.DateTime;
        var year = PersianCalendar.GetYear(dt);
        var month = PersianCalendar.GetMonth(dt);

        return period switch
        {
            ReportPeriodView.Daily => (reference.Date, reference.Date),
            ReportPeriodView.Weekly => GetWeekBounds(reference),
            ReportPeriodView.Monthly => GetMonthBounds(year, month),
            ReportPeriodView.Yearly => GetYearBounds(year),
            _ => (reference.Date, reference.Date)
        };
    }

    public static DateTimeOffset ShiftReference(ReportPeriodView period, DateTimeOffset reference, int direction)
    {
        return period switch
        {
            ReportPeriodView.Daily => reference.AddDays(direction),
            ReportPeriodView.Weekly => reference.AddDays(direction * 7),
            ReportPeriodView.Monthly => reference.AddMonths(direction),
            ReportPeriodView.Yearly => reference.AddYears(direction),
            _ => reference.AddDays(direction)
        };
    }

    public static string GetPeriodLabel(ReportPeriodView period, DateTimeOffset start, DateTimeOffset end)
    {
        return period == ReportPeriodView.Daily
            ? start.ToPersianDate()
            : $"{start.ToPersianDate()} - {end.ToPersianDate()}";
    }

    private static (DateTimeOffset Start, DateTimeOffset End) GetWeekBounds(DateTimeOffset reference)
    {
        var daysSinceSaturday = ((int)reference.DayOfWeek - (int)DayOfWeek.Saturday + 7) % 7;
        var start = reference.AddDays(-daysSinceSaturday).Date;
        var end = start.AddDays(6);
        return (start, end);
    }

    private static (DateTimeOffset Start, DateTimeOffset End) GetMonthBounds(int year, int month)
    {
        var start = FromPersianDate(year, month, 1);
        var end = month switch
        {
            <= 6 => FromPersianDate(year, month, 31),
            <= 11 => FromPersianDate(year, month, 30),
            _ => TryCreateDate(year, month, 30) ?? FromPersianDate(year, month, 29)
        };
        return (start, end);
    }

    private static (DateTimeOffset Start, DateTimeOffset End) GetYearBounds(int year)
    {
        var start = FromPersianDate(year, 1, 1);
        var end = TryCreateDate(year, 12, 30) ?? FromPersianDate(year, 12, 29);

        return (start, end);
    }

    private static DateTimeOffset FromPersianDate(int year, int month, int day)
    {
        var dateTime = PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        return new DateTimeOffset(dateTime, TimeSpan.Zero);
    }

    private static DateTimeOffset? TryCreateDate(int year, int month, int day)
    {
        try
        {
            return FromPersianDate(year, month, day);
        }
        catch
        {
            return null;
        }
    }

    private static bool SetPeriod(ReportPeriodView value, out ReportPeriodView period)
    {
        period = value;
        return true;
    }
}


