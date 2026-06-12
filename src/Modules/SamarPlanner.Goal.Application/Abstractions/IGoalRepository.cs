namespace SamarPlanner.Goal.Application.Abstractions;

public interface IGoalRepository
{
    Task<Guid?> CreateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken =  default);
    Task<Core.Entities.Goal?> GetAsTrackingAsync(Guid goalId,Guid userId, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default);
    Task<List<Core.Entities.Goal>> GetAllAsync(Guid userId,CancellationToken cancellationToken = default);
}