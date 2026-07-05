namespace SamarPlanner.Note.Application.Contracts;

public record NoteWithCategoryProjection(
    Guid NoteId,
    string? NoteTitle,
    string TextPreview,
    DateTime UpdateAt,
    Guid? CategoryId,
    string? CategoryTitle
);