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
}