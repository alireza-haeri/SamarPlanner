using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record RestoreTaskCommand(Guid TaskId, Guid UserId) : IRequest<Result<bool>>;