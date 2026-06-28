using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record FailGoalCommand(Guid UserId, Guid GoalId) : IRequest<Result<bool>>;