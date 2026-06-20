using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Task.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TaskType
{
    [EnumMember(Value = "Task")]
    Task = 1,
    [EnumMember(Value = "Event")]
    Event = 2
}