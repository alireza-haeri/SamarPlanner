using SamarPlanner.Task.Core.Entities;

namespace SamarPlanner.Task.Application.Abstractions;

public interface ITaskRepository
{
    Task<bool> CreateAsync(Core.Entities.Task task, CancellationToken cancellationToken = default);

    Task<Core.Entities.Task?> GetAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<Core.Entities.Task?> GetWithRepeatPatternAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<Core.Entities.Task?> GetWithOccurrencesAndRepeatPatternAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<Core.Entities.Task?> GetWithOccurrencesAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<Core.Entities.Task?> GetAsTrackingWithOutFilterAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(Core.Entities.Task task, CancellationToken cancellationToken = default);

    Task<bool> DeleteTaskWithOccurrencesWithOutFilterAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default);

    Task<List<Core.Entities.Task>> GetWithOccurrencesAndRepeatPatternAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken cancellationToken);
    Task<List<Core.Entities.Task>> GetDeletedTasksAsync(Guid userId,CancellationToken cancellationToken = default);
    Task<List<TaskOccurrence>> GetOccurrencesByGoalIdUntilDateAsync(Guid goalId, Guid userId, DateOnly periodStart,
        DateOnly periodEnd, CancellationToken ct = default);
}