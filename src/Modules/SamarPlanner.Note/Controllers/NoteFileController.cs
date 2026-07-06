using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Extensions;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Note.Controllers;

[Authorize]
[Route("/api/v1/note-files")]
public class NoteFileController(IMediator mediator) : BaseController
{
    [HttpGet("{noteFileId:guid}")]
    [SwaggerOperation(OperationId = "GetNoteFile")]
    public async Task<ActionResult<Result<GetNoteFileQueryResponse>>> GetFile(Guid noteFileId)
    {
        var result = await mediator.Send(new GetNoteFileQuery(UserId, noteFileId));
        return Result(result);
    }
}