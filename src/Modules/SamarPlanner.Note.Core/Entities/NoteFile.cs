using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SamarPlanner.Note.Core.Contracts;
using SamarPlanner.Shared.Contracts.Enums;

namespace SamarPlanner.Note.Core.Entities;

public class NoteFile
{
    public const string TableName = "NoteFiles";
    public const long MaxFileSizeBytes = 5 * 1024 * 1024;
    
    public Guid Id { get; private init; }
    public Guid NoteId { get; private set; }
    public string? Title { get; private set; }
    public string Extension { get; private set; } = null!;
    public NoteFileType Type { get; private set; }
    public long Lenght { get; private set; }

    public Note Note { get; private set; } = null!;

    public string FileName => $"{Id}.{Extension}";

    public static NoteFile Create(string? title, string contentType, long length)
        => new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Type = ResolveType(contentType),
            Extension = ResolveExtension(contentType),
            Lenght = length,
        };

    private static NoteFileType ResolveType(string contentType) =>
        contentType.StartsWith("image/") ? NoteFileType.Image : NoteFileType.Document;

    private static string ResolveExtension(string contentType) =>
        AllowedFileTypes.ContentTypeToExtension.TryGetValue(contentType, out var extension)
            ? extension
            : throw new ArgumentException($"Content type '{contentType}' is not allowed.");
}