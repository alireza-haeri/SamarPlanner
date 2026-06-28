using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Goal.Core.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GoalStatus
{
    [EnumMember(Value = "Active")]
    Active = 0,
    [EnumMember(Value = "Achieved")]
    Achieved = 1,
    [EnumMember(Value = "Failed")]
    Failed = 2,
    [EnumMember(Value = "RolledOver")]
    RolledOver = 3
}