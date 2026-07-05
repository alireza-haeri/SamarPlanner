using SamarPlanner.Note.Core.Entities;

namespace SamarPlanner.Note.Application.Abstractions;

public interface IFileStorageService
{
    Task<bool> CreateFileAsync(NoteFile file, byte[] content, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<bool> DeleteFileAsync(NoteFile noteFile, CancellationToken none);
    Task<byte[]> ReadFileAsync(NoteFile noteFile, CancellationToken cancellationToken = default);
}