using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Commands.CreateNote;

public class CreateNoteCommandHandler(INoteRepository noteRepository, IFileStorageService fileStorageService)
    : IRequestHandler<CreateNoteCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var files = request.Files
            .Select(f => NoteFile.Create(f.Title, f.ContentType, f.Length))
            .ToList();

        var uploaded = new List<NoteFile>();
        foreach (var (dto, entity) in request.Files.Zip(files))
        {
            var success = await fileStorageService.CreateFileAsync(entity, dto.Content, cancellationToken);
            if (!success)
            {
                await DeleteFilesAsync(uploaded);
                return Result<Guid>.GeneralFailure("خطایی در ذخیره فایل‌ها رخ داد.");
            }

            uploaded.Add(entity);
        }

        var note = Core.Entities.Note.Create(request.UserId, request.Title, request.Text, files, request.CategoryId);
        var createResult = await noteRepository.CreateAsync(note, cancellationToken);
        if (createResult is null)
        {
            await DeleteFilesAsync(files);
            return Result<Guid>.GeneralFailure("خطایی در ایجاد یاداشت رخ داد.");
        }

        return Result<Guid>.Success(createResult.Value);
    }

    private System.Threading.Tasks.Task DeleteFilesAsync(IEnumerable<NoteFile> files)
    {
        return System.Threading.Tasks.Task.WhenAll(files.Select(f =>
            fileStorageService.DeleteFileAsync(f, CancellationToken.None)));
    }
}