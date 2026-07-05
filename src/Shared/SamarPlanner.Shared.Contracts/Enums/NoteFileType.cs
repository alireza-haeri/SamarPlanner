using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SamarPlanner.Shared.Contracts.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NoteFileType
{
    [EnumMember(Value = "Image")] Image,
    [EnumMember(Value = "Document")] Document
}