using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record ChangeTaskOccurrenceSkipCommand(Guid TaskId, Guid UserId, DateOnly Date,bool IsSkipped) : IRequest<Result<bool>>;