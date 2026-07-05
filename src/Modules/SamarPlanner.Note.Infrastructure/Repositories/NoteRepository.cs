using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Application.Contracts;
using SamarPlanner.Note.Infrastructure.Persistence;

namespace SamarPlanner.Note.Infrastructure.Repositories;

public class NoteRepository(NoteDbContext context, ILogger<NoteRepository> logger) : INoteRepository
{
    public async Task<Guid?> CreateAsync(Core.Entities.Note note, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.AddAsync(note, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return note.Id;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to create note for user with id {NoteUserId}", note.UserId);
            return null;
        }
    }

    public async Task<Core.Entities.Note?> GetByIdAsTrackingAsync(Guid userId, Guid noteId,
        CancellationToken cancellationToken = default)
    {
        return await context.Notes
            .AsTracking()
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId, cancellationToken);
    }

    public async Task<Core.Entities.Note?> GetByIdWithFilesAsync(Guid userId, Guid noteId,
        CancellationToken cancellationToken = default)
    {
        return await context.Notes
            .AsNoTracking()
            .Include(n => n.Files)
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId, cancellationToken);
    }

    public async Task<bool> UpdateAsync(Core.Entities.Note note, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Update(note);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to update note for user with id {NoteUserId}", note.UserId);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Core.Entities.Note note, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Remove(note);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to delete note for user with id {NoteUserId}", note.UserId);
            return false;
        }
    }

    public async Task<List<NoteWithCategoryProjection>> GetAllWithCategoryByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var maxTextPreviewLength = Core.Entities.Note.MaxTextPreviewLength;
        return await context.Notes
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .Include(n => n.Category)
            .Select(n => new NoteWithCategoryProjection(
                n.Id,
                n.Title,
                n.Text.Length > maxTextPreviewLength ? n.Text.Substring(0, maxTextPreviewLength) : n.Text,
                n.UpdateAt,
                n.CategoryId,
                n.CategoryId == null ? null : n.Category!.Title)
            )
            .ToListAsync(cancellationToken);
    }
}