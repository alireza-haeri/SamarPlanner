using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record ChangeOccurrenceDateCommand(Guid TaskId,Guid UserId,DateOnly OldDate, DateOnly NewDate) : IRequest<Result<bool>>;