using SamarPlanner.Shared.Contracts.Contracts;

namespace SamarPlanner.Note.Contracts;

public record CreateNoteRequest( string? Title, string Text, List<NoteFileDto> Files, Guid? CategoryId);