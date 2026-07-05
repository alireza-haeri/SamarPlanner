using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Note.Infrastructure.Persistence;

namespace SamarPlanner.Note.Infrastructure.Repositories;

public class NoteFileRepository(NoteDbContext context) : INoteFileRepository
{
    public async Task<NoteFile?> GetByIdAsync(Guid userId, Guid noteFileId,
        CancellationToken cancellationToken = default)
    {
        return await context.NoteFiles
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == noteFileId && f.Note.UserId == userId, cancellationToken);
    }
}