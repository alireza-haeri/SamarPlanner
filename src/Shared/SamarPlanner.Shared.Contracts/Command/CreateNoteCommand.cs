#region

using MediatR;
using SamarPlanner.Shared.Contracts.Contracts;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record CreateNoteCommand(Guid UserId, string? Title, string Text, List<NoteFileDto> Files, Guid? CategoryId)
    : IRequest<Result<Guid>>;