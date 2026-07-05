using SamarPlanner.Note.Core.Entities;

namespace SamarPlanner.Note.Application.Abstractions;

public interface INoteFileRepository
{
    Task<NoteFile?> GetByIdAsync(Guid userId, Guid noteFileId, CancellationToken cancellationToken = default);
}