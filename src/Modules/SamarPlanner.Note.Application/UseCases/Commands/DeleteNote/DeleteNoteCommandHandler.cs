using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Commands.DeleteNote;

public class DeleteNoteCommandHandler(INoteRepository noteRepository, IFileStorageService fileStorageService)
: IRequestHandler<DeleteNoteCommand,Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await noteRepository.GetByIdAsTrackingAsync(request.UserId, request.NoteId, cancellationToken);
        if (note is null)
            return Result<bool>.NotfoundFailure("یادداشت مورد نظر یافت نشد.");

        var deleteResult = await noteRepository.DeleteAsync(note, cancellationToken);
        if (!deleteResult)
            return Result<bool>.GeneralFailure("خطایی در حذف یادداشت رخ داد.");

        await DeleteFilesAsync(note.Files);

        return Result<bool>.Success(true);
    }
    
    private System.Threading.Tasks.Task DeleteFilesAsync(IEnumerable<NoteFile> files)
    {
        return System.Threading.Tasks.Task.WhenAll(files.Select(f =>
            fileStorageService.DeleteFileAsync(f, CancellationToken.None)));
    }
}