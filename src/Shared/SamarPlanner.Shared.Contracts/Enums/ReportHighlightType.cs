using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Shared.Contracts.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ReportHighlightType
{
    [EnumMember(Value = "Negative")]
    Negative = 0,
    [EnumMember(Value = "Positive")]
    Positive = 1,
    [EnumMember(Value = "Lesson")]
    Lesson = 2
}