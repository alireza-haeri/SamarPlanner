using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record DeleteNoteCommand(Guid UserId, Guid NoteId) : IRequest<Result<bool>>;