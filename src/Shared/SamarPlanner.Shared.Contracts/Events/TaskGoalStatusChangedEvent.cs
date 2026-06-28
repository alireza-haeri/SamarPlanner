using MediatR;

namespace SamarPlanner.Shared.Contracts.Events;

public record TaskGoalStatusChangedEvent(
    Guid TaskId,
    Guid UserId,
    Guid GoalId
) : INotification;