using Microsoft.EntityFrameworkCore;
using SamarPlanner.Goal.Infrastructure.Persistence;

namespace SamarPlanner.Goal.Infrastructure.Repositories;

public class GoalRepository(GoalDbContext context) : Application.Abstractions.IGoalRepository
{
    public async Task<Guid?> CreateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(goal, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return goal.Id;
    }

    public async Task<Core.Entities.Goal?> GetAsTrackingAsync(Guid goalId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        var goal = await context.Goals
            .AsTracking()
            .FirstOrDefaultAsync(x =>
                    x.Id == goalId && x.UserId == userId
                , cancellationToken
            );
        return goal;
    }

    public async Task<bool> UpdateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default)
    {
        context.Update(goal);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default)
    {
        context.Remove(goal);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<List<Core.Entities.Goal>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var goals = await context.Goals
            .AsNoTracking()
            .Where(g => g.UserId == userId)
            .ToListAsync(cancellationToken);
        
        return goals;
    }
}