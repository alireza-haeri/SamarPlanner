using SamarPlanner.Note.Core.Entities;

namespace SamarPlanner.Note.Application.Abstractions;

public interface INoteCategoryRepository
{
    Task<Guid?> CreateAsync(NoteCategory noteCategory, CancellationToken cancellationToken = default);
    Task<NoteCategory?> GetByIdAsyncAsTracking(Guid userId, Guid noteCategoryId, CancellationToken cancellationToken  = default);
    Task<bool> UpdateAsync(NoteCategory noteCategory, CancellationToken cancellationToken  = default);
    Task<bool> DeleteAsync(NoteCategory noteCategory, CancellationToken cancellationToken  = default);
    Task<List<NoteCategory>> GetUserCategoryAsync(Guid userId, CancellationToken cancellationToken  = default);
}