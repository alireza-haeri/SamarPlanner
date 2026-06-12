using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record DeleteGoalCommand(Guid GoalId, Guid UserId)
    :IRequest<Result<bool>>;