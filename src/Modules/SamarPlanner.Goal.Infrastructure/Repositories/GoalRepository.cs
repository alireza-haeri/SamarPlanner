using Microsoft.EntityFrameworkCore;
using SamarPlanner.Goal.Application.Dtos;
using SamarPlanner.Goal.Core.Enums;
using SamarPlanner.Goal.Infrastructure.Persistence;

namespace SamarPlanner.Goal.Infrastructure.Repositories;

public class GoalRepository(GoalDbContext context) : Application.Abstractions.IGoalRepository
{
    public async Task<Guid?> CreateAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.AddAsync(goal, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return goal.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
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
        try
        {
            context.Update(goal);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteWithChildrenAsync(Core.Entities.Goal goal, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.Goals
                .IgnoreQueryFilters()
                .Where(o => o.ParentGoalId == goal.Id)
                .ExecuteDeleteAsync(cancellationToken);

            var deleted = await context.Goals
                .IgnoreQueryFilters()
                .Where(t => t.Id == goal.Id)
                .ExecuteDeleteAsync(cancellationToken);
            
            return deleted > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<List<Core.Entities.Goal>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var goals = await context.Goals
            .AsNoTracking()
            .Where(g => g.UserId == userId)
            .ToListAsync(cancellationToken);

        return goals;
    }

    public async Task<List<ShortGoal>> GetAllShortAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var goals = await context.Goals
            .AsNoTracking()
            .Where(g => g.UserId == userId && g.Status == GoalStatus.Active)
            .Select(g => new ShortGoal(g.Id, g.Title, g.GoalPriority))
            .ToListAsync(cancellationToken);

        return goals;
    }

    public async Task<List<Core.Entities.Goal>> GetNotRolledOverChildGoalsAsTrackingAsync(Guid goalId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.Goals
            .AsTracking()
            .Where(g => g.ParentGoalId == goalId && g.UserId == userId && g.Status != GoalStatus.RolledOver)
            .ToListAsync(cancellationToken);
    }
}