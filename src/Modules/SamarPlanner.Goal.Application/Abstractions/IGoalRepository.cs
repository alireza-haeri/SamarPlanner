using SamarPlanner.Goal.Application.Dtos;

namespace SamarPlanner.Goal.Application.Abstractions;

public interface IGoalRepository
{
    Task<Guid?> CreateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken =  default);
    Task<Core.Entities.Goal?> GetAsTrackingAsync(Guid goalId,Guid userId, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default);
    Task<bool> DeleteWithChildrenAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default);
    Task<List<Core.Entities.Goal>> GetAllAsync(Guid userId,CancellationToken cancellationToken = default);
    Task<List<ShortGoal>> GetAllShortAsync(Guid userId,CancellationToken cancellationToken = default);
    Task<List<Core.Entities.Goal>> GetNotRolledOverChildGoalsAsTrackingAsync(Guid goalId, Guid userId, CancellationToken cancellationToken);
}
