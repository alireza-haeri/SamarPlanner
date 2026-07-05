namespace SamarPlanner.Shared.Contracts.Contracts;

public record NoteFileDto(string? Title, string ContentType, byte[] Content, long Length);