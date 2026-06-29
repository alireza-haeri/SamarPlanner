using System.Globalization;

namespace SamarPlanner.Web.Helpers;

public static class PersianDateHelper
{
    private static readonly PersianCalendar PersianCalendar = new();

    // تبدیل میلادی به شمسی (با فرمت yyyy/MM/dd)
    public static string ToPersianDate(this DateTimeOffset dateTime)
    {
        var dt = dateTime.DateTime;
        int year = PersianCalendar.GetYear(dt);
        int month = PersianCalendar.GetMonth(dt);
        int day = PersianCalendar.GetDayOfMonth(dt);
        return $"{year:0000}/{month:00}/{day:00}";
    }

    public static string ToPersianDate(this DateTime dateTime)
    {
        int year = PersianCalendar.GetYear(dateTime);
        int month = PersianCalendar.GetMonth(dateTime);
        int day = PersianCalendar.GetDayOfMonth(dateTime);
        return $"{year:0000}/{month:00}/{day:00}";
    }

    public static string ToPersianDate(this DateOnly dateOnly)
    {
        var dt = dateOnly.ToDateTime(TimeOnly.MinValue);
        int year = PersianCalendar.GetYear(dt);
        int month = PersianCalendar.GetMonth(dt);
        int day = PersianCalendar.GetDayOfMonth(dt);
        return $"{year:0000}/{month:00}/{day:00}";
    }

    // تبدیل شمسی به میلادی (با زمان 00:00:00)
    public static DateTimeOffset? ToGregorianDateTime(string persianDate)
    {
        if (string.IsNullOrEmpty(persianDate))
            return null;

        var parts = persianDate.Split('/');
        if (parts.Length != 3)
            return null;

        if (!int.TryParse(parts[0], out int year) ||
            !int.TryParse(parts[1], out int month) ||
            !int.TryParse(parts[2], out int day))
            return null;

        try
        {
            var dt = PersianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            return new DateTimeOffset(dt, TimeSpan.Zero);
        }
        catch
        {
            return null;
        }
    }

    // دریافت تاریخ امروز به شمسی
    public static string TodayPersian()
    {
        return DateTime.Now.ToPersianDate();
    }

    // دریافت تاریخ امروز به میلادی (برای استفاده در API)
    public static DateTimeOffset TodayGregorian()
    {
        return DateTimeOffset.Now.Date;
    }
    
    public static string GetWeekday(DateTimeOffset date)
    {
        var dt = date.DateTime;
        var dayOfWeek = PersianCalendar.GetDayOfWeek(dt);
        return dayOfWeek switch
        {
            DayOfWeek.Saturday => "شنبه",
            DayOfWeek.Sunday => "یکشنبه",
            DayOfWeek.Monday => "دوشنبه",
            DayOfWeek.Tuesday => "سه‌شنبه",
            DayOfWeek.Wednesday => "چهارشنبه",
            DayOfWeek.Thursday => "پنجشنبه",
            DayOfWeek.Friday => "جمعه",
            _ => ""
        };
    }

    public static string GetMonthName(DateTimeOffset date)
    {
        var dt = date.DateTime;
        var month = PersianCalendar.GetMonth(dt);
        return month switch
        {
            1 => "فروردین",
            2 => "اردیبهشت",
            3 => "خرداد",
            4 => "تیر",
            5 => "مرداد",
            6 => "شهریور",
            7 => "مهر",
            8 => "آبان",
            9 => "آذر",
            10 => "دی",
            11 => "بهمن",
            12 => "اسفند",
            _ => ""
        };
    }
}