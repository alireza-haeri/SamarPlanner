using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Note.Application.Contracts;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetUserNotesGroupedByCategory;

public class GetUserNotesGroupedByCategoryQueryHandler(INoteRepository noteRepository)
    : IRequestHandler<GetUserNotesGroupedByCategoryQuery, Result<GetUserNotesGroupedByCategoryQueryResponse>>
{
    public async Task<Result<GetUserNotesGroupedByCategoryQueryResponse>> Handle(
        GetUserNotesGroupedByCategoryQuery request, CancellationToken cancellationToken)
    {
        var notes = await noteRepository.GetAllWithCategoryByUserIdAsync(request.UserId, cancellationToken);
        if (!notes.Any())
            return Result<GetUserNotesGroupedByCategoryQueryResponse>
                .Success(new GetUserNotesGroupedByCategoryQueryResponse([]));
        var result =
            notes.GroupBy(n => new { n.CategoryId, n.CategoryTitle })
                .Select(g =>
                    new GetUserNotesGroupedByCategoryQueryResponseCategory(
                        CategoryId: g.Key.CategoryId,
                        Title: g.Key.CategoryTitle,
                        Notes: g.Select(n =>
                                new GetUserNotesGroupedByCategoryQueryResponseNote(
                                    n.NoteId,
                                    n.NoteTitle,
                                    n.TextPreview,
                                    n.UpdateAt)
                            ).OrderByDescending(n => n.UpdatedAt)
                            .ToList()
                    )
                ).OrderBy(g => g.CategoryId is null ? 1 : 0)
                .ToList();

        return Result<GetUserNotesGroupedByCategoryQueryResponse>
            .Success(new GetUserNotesGroupedByCategoryQueryResponse(result));
    }
}