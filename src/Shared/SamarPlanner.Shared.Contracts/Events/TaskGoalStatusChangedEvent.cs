using MediatR;

namespace SamarPlanner.Shared.Contracts.Events;

public record TaskGoalStatusChangedEvent(
    Guid UserId,
    Guid GoalId
) : INotification;