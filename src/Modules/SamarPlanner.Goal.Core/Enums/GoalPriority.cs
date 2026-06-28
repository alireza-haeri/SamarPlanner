using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Goal.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GoalPriority
{
    [EnumMember(Value = "Low")]
    Low = 1,
    [EnumMember(Value = "Medium")]
    Medium = 2,
    [EnumMember(Value = "High")]
    High = 3
}
