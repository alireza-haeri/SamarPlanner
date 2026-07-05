using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetNoteFileQuery(Guid UserId, Guid NoteFileId) 
    : IRequest<Result<GetNoteFileQueryResponse>>;

public record GetNoteFileQueryResponse(byte[] Content, string ContentType, string FileName);