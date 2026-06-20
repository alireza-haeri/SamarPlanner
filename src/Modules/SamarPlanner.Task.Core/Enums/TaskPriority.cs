using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Task.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskPriority
{
    [EnumMember(Value = "High")]
    High = 1,
    [EnumMember(Value = "Medium")]
    Medium = 2,
    [EnumMember(Value = "Low")]
    Low = 3
}