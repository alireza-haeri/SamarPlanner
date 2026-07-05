using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Infrastructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILogger<FileStorageService> _logger;

    private readonly NoteFilesSettings _settings;

    private string NoteFilesPath => Path.Combine(_webHostEnvironment.ContentRootPath, _settings.FilesPath);

    public FileStorageService(IOptions<ApplicationSettings> options, IWebHostEnvironment webHostEnvironment,
        ILogger<FileStorageService> logger)
    {
        _settings = options.Value.NoteFiles ??
                    throw new InvalidOperationException(nameof(ApplicationSettings.NoteFiles));
        _webHostEnvironment = webHostEnvironment;
        _logger = logger;

        Directory.CreateDirectory(NoteFilesPath);
    }

    public async Task<bool> CreateFileAsync(NoteFile file, byte[] content,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await File.WriteAllBytesAsync(GetFilePath(file), content, cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create file with name {FileName}", file.FileName);
            return false;
        }
    }

    public async Task<bool> DeleteFileAsync(NoteFile noteFile, CancellationToken none)
    {
        try
        {
            var filePath = GetFilePath(noteFile);
            if (File.Exists(filePath))
                File.Delete(filePath);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete file with name {FileName}", noteFile.FileName);
            return false;
        }
    }

    public async Task<byte[]> ReadFileAsync(NoteFile noteFile, CancellationToken cancellationToken = default)
    {
        try
        {
            var filePath = GetFilePath(noteFile);
            if (!File.Exists(filePath))
                return [];

            return await File.ReadAllBytesAsync(filePath, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to read file with name {FileName}", noteFile.FileName);
            return [];
        }
    }

    private string GetFilePath(NoteFile file) =>
        Path.Combine(NoteFilesPath, file.FileName);
}