using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record SoftDeleteTaskOccurrenceCommand(Guid  TaskId, Guid UserId, DateOnly Date) :  IRequest<Result<bool>>;