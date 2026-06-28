using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Command;

public record AchieveGoalCommand(Guid UserId,Guid GoalId) : IRequest<Result<bool>>;