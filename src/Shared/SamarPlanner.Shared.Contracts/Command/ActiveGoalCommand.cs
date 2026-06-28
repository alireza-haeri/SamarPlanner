using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record ActiveGoalCommand(Guid UserId, Guid GoalId) : IRequest<Result<bool>>;