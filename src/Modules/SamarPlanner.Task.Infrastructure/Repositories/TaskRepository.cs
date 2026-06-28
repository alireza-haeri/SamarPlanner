using Microsoft.EntityFrameworkCore;
using SamarPlanner.Task.Application.Abstractions;
using SamarPlanner.Task.Core.Entities;
using SamarPlanner.Task.Infrastructure.Persistence;

namespace SamarPlanner.Task.Infrastructure.Repositories;

public class TaskRepository(TaskDbContext context) : ITaskRepository
{
    public async Task<bool> CreateAsync(Core.Entities.Task task, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Add(task);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<Core.Entities.Task?> GetAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsTracking()
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<Core.Entities.Task?> GetWithRepeatPatternAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsTracking()
            .Include(t => t.RepeatPattern)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<Core.Entities.Task?> GetWithOccurrencesAndRepeatPatternAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsTracking()
            .Include(t => t.RepeatPattern)
            .Include(t => t.Occurrences)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<Core.Entities.Task?> GetWithOccurrencesAsTrackingAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsTracking()
            .Include(t => t.Occurrences)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<Core.Entities.Task?> GetWithOccurrencesAsTrackingWithOutFilterAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsTracking().IgnoreQueryFilters()
            .Include(t => t.Occurrences)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<Core.Entities.Task?> GetAsTrackingWithOutFilterAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Core.Entities.Task task, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteTaskWithOccurrencesWithOutFilterAsync(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var taskExists = await context.Tasks
                .IgnoreQueryFilters()
                .AnyAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);

            if (!taskExists)
                return false;

            await context.TaskOccurrences
                .IgnoreQueryFilters()
                .Where(o => o.TaskId == taskId)
                .ExecuteDeleteAsync(cancellationToken);

            var deleted = await context.Tasks
                .IgnoreQueryFilters()
                .Where(t => t.Id == taskId && t.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);

            return deleted > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> DeleteOccurrencesWithOutFilterAsync(Guid taskId, Guid userId, DateOnly date,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var taskExists = await context.Tasks
                .IgnoreQueryFilters()
                .AnyAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);

            if (!taskExists)
                return false;

            var deleted = await context.TaskOccurrences
                .IgnoreQueryFilters()
                .Where(o => o.TaskId == taskId && o.Date == date)
                .ExecuteDeleteAsync(cancellationToken);

            return deleted > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<List<Core.Entities.Task>> GetWithOccurrencesAndRepeatPatternAsync(Guid userId, DateOnly from,
        DateOnly to,
        CancellationToken cancellationToken)
    {
        return await context.Tasks
            .AsNoTracking()
            .Where(t => t.UserId == userId && (
                t.Occurrences.Any(o => o.Date >= from && o.Date <= to) ||
                (t.RepeatPattern != null && t.RepeatPattern.AnchorDate <= to)
            ))
            .Include(t => t.RepeatPattern)
            .Include(t => t.Occurrences.Where(o => o.Date >= from && o.Date <= to))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Core.Entities.Task>> GetDeletedTasksAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(t => t.UserId == userId && t.SoftDeleted == true)
            .ToListAsync(cancellationToken);
    }

    public async Task<Core.Entities.Task?> GetWithOccurrences(Guid taskId, Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.Tasks.AsNoTracking()
            .Include(t => t.Occurrences)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    }

    public Task<List<Guid>> GetTaskIdsByGoalIdAsync(Guid goalId, CancellationToken cancellationToken = default)
    {
        return context.Tasks.AsNoTracking()
            .Where(t => t.ParentGoalId == goalId)
            .Select(t => t.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TaskOccurrence>> GetOccurrencesByGoalIdUntilDateAsync(Guid goalId, Guid userId,
        DateOnly periodStart, DateOnly periodEnd, CancellationToken ct = default)
    {
        return await context.Tasks.AsNoTracking()
            .Include(t => t.Occurrences)
            .Where(t => t.UserId == userId && t.ParentGoalId == goalId)
            .SelectMany(t => t.Occurrences.Where(o => o.Date >= periodStart && o.Date <= periodEnd && !o.IsSkipped))
            .ToListAsync(ct);
    }
}