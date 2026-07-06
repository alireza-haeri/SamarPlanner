using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Commands.UpdateNote;

public class UpdateNoteCommandHandler(INoteRepository noteRepository, IFileStorageService fileStorageService)
    : IRequestHandler<UpdateNoteCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await noteRepository.GetByIdAsTrackingWithFilesAsync(request.UserId, request.NoteId, cancellationToken);
        if (note is null)
            return Result<bool>.NotfoundFailure("یادداشت مورد نظر یافت نشد.");

        var allRemovedFilesExist = request.RemovedFiles.All(id => note.Files.Any(f => f.Id == id));
        if (!allRemovedFilesExist)
            return Result<bool>.NotfoundFailure("برخی از فایل‌های مورد نظر برای حذف یافت نشد.");

        var filesToRemove = note.Files.Where(f => request.RemovedFiles.Contains(f.Id)).ToList();

        var newFileEntities = request.NewFiles
            .Select(f => NoteFile.Create(f.Title, f.ContentType, f.Length))
            .ToList();

        var uploadedFiles = new List<NoteFile>();
        foreach (var (dto, entity) in request.NewFiles.Zip(newFileEntities))
        {
            var uploadResult = await fileStorageService.CreateFileAsync(entity, dto.Content, cancellationToken);
            if (!uploadResult)
            {
                await DeleteFilesAsync(uploadedFiles);
                return Result<bool>.GeneralFailure("خطایی در ذخیره فایل‌ها رخ داد.");
            }

            uploadedFiles.Add(entity);
        }

        note.RemoveFiles(filesToRemove);
        note.Update(request.Title, request.Text, request.CategoryId, newFileEntities);

        var updateResult = await noteRepository.UpdateAsync(note, cancellationToken);
        if (!updateResult)
        {
            await DeleteFilesAsync(uploadedFiles);
            return Result<bool>.GeneralFailure("خطایی در بروزرسانی یادداشت رخ داد.");
        }

        await DeleteFilesAsync(filesToRemove);

        return Result<bool>.Success(true);
    }

    private System.Threading.Tasks.Task DeleteFilesAsync(IEnumerable<NoteFile> files)
    {
        return System.Threading.Tasks.Task.WhenAll(files.Select(f =>
            fileStorageService.DeleteFileAsync(f, CancellationToken.None)));
    }
}