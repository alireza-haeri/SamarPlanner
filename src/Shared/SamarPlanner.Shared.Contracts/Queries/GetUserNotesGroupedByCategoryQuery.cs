#region

using MediatR;
using SamarPlanner.Shared.Kernel;

#endregion

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetUserNotesGroupedByCategoryQuery(Guid UserId)
    : IRequest<Result<GetUserNotesGroupedByCategoryQueryResponse>>;

public record GetUserNotesGroupedByCategoryQueryResponse(
    List<GetUserNotesGroupedByCategoryQueryResponseCategory> Groups
);

public record GetUserNotesGroupedByCategoryQueryResponseCategory(
    Guid? CategoryId,
    string? Title,
    List<GetUserNotesGroupedByCategoryQueryResponseNote> Notes
);

public record GetUserNotesGroupedByCategoryQueryResponseNote(
    Guid NoteId,
    string? Title,
    string TextPreview,
    DateTime UpdatedAt
);