using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record RestoreTaskOccurrenceCommand(Guid TaskId, Guid UserId, DateOnly Date) : IRequest<Result<bool>>;