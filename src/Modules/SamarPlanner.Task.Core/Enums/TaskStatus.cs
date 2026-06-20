using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Task.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskStatus
{
    [EnumMember(Value = "Pending")]
    Pending = 1,
    [EnumMember(Value = "Done")]
    Done = 2,
    [EnumMember(Value = "NotDone")]
    NotDone = 3,
    [EnumMember(Value = "AlmostDone")]
    AlmostDone = 4,
}