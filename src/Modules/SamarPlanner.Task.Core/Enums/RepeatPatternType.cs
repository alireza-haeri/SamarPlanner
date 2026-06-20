using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Task.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RepeatPatternType
{
    [EnumMember(Value = "Daily")]
    Daily = 1,
    [EnumMember(Value = "WeeklyOnDays")]
    WeeklyOnDays = 2,
    [EnumMember(Value = "MonthlyOnDays")]
    MonthlyOnDays = 3,
}