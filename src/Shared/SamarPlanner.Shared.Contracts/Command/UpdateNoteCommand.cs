#region

using MediatR;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record UpdateNoteCommand(
    Guid UserId,
    Guid NoteId,
    string? Title,
    string Text,
    Guid? CategoryId,
    List<NoteFileDto> NewFiles,
    List<Guid> RemovedFiles)
    : IRequest<Result<bool>>;