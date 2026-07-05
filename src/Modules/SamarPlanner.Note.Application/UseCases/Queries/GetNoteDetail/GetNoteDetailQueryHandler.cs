using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetNoteDetail;

public class GetNoteDetailQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetNoteDetailQuery, Result<GetNoteDetailQueryResponse>>
{
    public async Task<Result<GetNoteDetailQueryResponse>> Handle(GetNoteDetailQuery request,
        CancellationToken cancellationToken)
    {
        var note = await noteRepository.GetByIdWithFilesAsync(request.UserId, request.NoteId, cancellationToken);
        if (note is null)
            return Result<GetNoteDetailQueryResponse>.NotfoundFailure("یادداشت مورد نظر یافت نشد.");

        return Result<GetNoteDetailQueryResponse>.Success(new GetNoteDetailQueryResponse(
            note.Id,
            note.Title,
            note.Text,
            note.UpdateAt,
            note.CategoryId,
            note.Files.Select(f => new GetNoteDetailQueryResponseFile(
                f.Id,
                f.Title,
                f.Type
            )).ToList()
        ));
    }
}