using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Core.Contracts;
using SamarPlanner.Shared.Contracts.Enums;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetNoteFile;

public class GetNoteFileQueryHandler(INoteFileRepository noteFileRepository, IFileStorageService fileStorageService)
    : IRequestHandler<GetNoteFileQuery, Result<GetNoteFileQueryResponse>>
{
    public async Task<Result<GetNoteFileQueryResponse>> Handle(GetNoteFileQuery request,
        CancellationToken cancellationToken)
    {
        var noteFile = await noteFileRepository.GetByIdAsync(request.UserId, request.NoteFileId, cancellationToken);
        if (noteFile == null)
            return Result<GetNoteFileQueryResponse>.NotfoundFailure("فایل مورد نظر یافت نشد.");

        var noteFileContent = await fileStorageService.ReadFileAsync(noteFile, cancellationToken);
        if (!noteFileContent.Any())
            return Result<GetNoteFileQueryResponse>.GeneralFailure("خطایی در دریافت فایل رخ داد.");

        return Result<GetNoteFileQueryResponse>.Success(new GetNoteFileQueryResponse(
            noteFileContent, ResolveContentType(noteFile.Extension), noteFile.FileName));
    }

    private static string ResolveContentType(string extension)
    {
        return AllowedFileTypes.ContentTypeToExtension.FirstOrDefault(a => a.Value == extension).Key
               ?? "application/octet-stream";
    }
}