using SamarPlanner.Shared.Contracts.Contracts;

namespace SamarPlanner.Note.Contracts;

public record UpdateNoteRequest(
    string? Title,
    string Text,
    Guid? CategoryId,
    List<NoteFileDto> NewFiles,
    List<Guid> RemovedFiles);