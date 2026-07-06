using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Note.Infrastructure.Persistence;

namespace SamarPlanner.Note.Infrastructure.Repositories;

public class NoteCategoryRepository(NoteDbContext context, ILogger<NoteCategoryRepository> logger)
    : INoteCategoryRepository
{
    public async Task<Guid?> CreateAsync(NoteCategory noteCategory, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.AddAsync(noteCategory, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return noteCategory.Id;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to create note category with userID {NoteCategoryUserId}", noteCategory.UserId);
            return null;
        }
    }

    public async Task<NoteCategory?> GetByIdAsyncAsTracking(Guid userId, Guid noteCategoryId,
        CancellationToken cancellationToken = default)
    {
        return await context.NoteCategories
            .AsTracking()
            .FirstOrDefaultAsync(n => n.UserId == userId && n.Id == noteCategoryId, cancellationToken);
    }

    public async Task<bool> UpdateAsync(NoteCategory noteCategory, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Update(noteCategory);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to update note category with  id {NoteCategoryId}", noteCategory.Id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(NoteCategory noteCategory, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Remove(noteCategory);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to delete note category with  id {NoteCategoryId}", noteCategory.Id);
            return false;
        }
    }

    public async Task<List<NoteCategory>> GetUserCategoryAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.NoteCategories
            .Where(n => n.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}