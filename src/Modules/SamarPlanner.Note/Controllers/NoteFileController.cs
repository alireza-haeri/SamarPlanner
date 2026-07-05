using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Extensions;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Controllers;

[Authorize]
[Route("/api/v1/note-files")]
public class NoteFileController(IMediator mediator) : BaseController
{
    [HttpGet("{noteFileId:guid}")]
    public async Task<ActionResult<Result<GetNoteFileQueryResponse>>> GetFile(Guid noteFileId)
    {
        var result = await mediator.Send(new GetNoteFileQuery(UserId, noteFileId));
        if (!result.IsSuccess)
            return Ok(result);

        var response = result.Response!;
        return File(response.Content, response.ContentType, response.FileName);
    }
}