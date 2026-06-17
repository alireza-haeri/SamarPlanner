using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record ChangeOccurrenceTimeCommand(Guid TaskId, Guid UserId, DateOnly Date, TimeOnly NewTime) : IRequest<Result<bool>>;