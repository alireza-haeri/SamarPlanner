using SamarPlanner.Note.Application.Contracts;

namespace SamarPlanner.Note.Application.Abstractions;

public interface INoteRepository
{
    Task<Guid?> CreateAsync(Core.Entities.Note note, CancellationToken cancellationToken = default);
    Task<Core.Entities.Note?> GetByIdAsTrackingWithFilesAsync(Guid userId, Guid noteId, CancellationToken cancellationToken = default);
    Task<Core.Entities.Note?> GetByIdWithFilesAsync(Guid userId, Guid noteId, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Core.Entities.Note note, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Core.Entities.Note note, CancellationToken cancellationToken  = default);
    Task<List<NoteWithCategoryProjection>> GetAllWithCategoryByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}