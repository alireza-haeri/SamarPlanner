#region

using MediatR;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetNoteDetailQuery(Guid UserId, Guid NoteId) : IRequest<Result<GetNoteDetailQueryResponse>>;

public record GetNoteDetailQueryResponse(
    Guid NoteId,
    string? Title,
    string Text,
    DateTime UpdateAt,
    Guid? CategoryId,
    List<GetNoteDetailQueryResponseFile> Files);

public record GetNoteDetailQueryResponseFile(Guid NoteFileId, string? Title, NoteFileType FileType);