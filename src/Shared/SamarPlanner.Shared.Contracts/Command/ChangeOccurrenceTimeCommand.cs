#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record ChangeOccurrenceTimeCommand(Guid TaskId, Guid UserId, DateOnly Date, TimeOnly NewTime)
    : IRequest<Result<bool>>;