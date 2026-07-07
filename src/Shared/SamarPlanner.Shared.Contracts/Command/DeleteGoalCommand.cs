#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Command;

public record DeleteGoalCommand(Guid GoalId, Guid UserId)
    : IRequest<Result<bool>>;