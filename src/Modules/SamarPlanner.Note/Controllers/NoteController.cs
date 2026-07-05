using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Note.Contracts;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Note.Controllers;

[Authorize]
[Route("/api/v1/notes")]
public class NoteController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateNote")]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateNoteRequest request)
    {
        var result = await mediator.Send(new CreateNoteCommand(
            UserId: UserId,
            Title: request.Title,
            Text: request.Text,
            Files: request.Files,
            CategoryId: request.CategoryId)
        );
        return Result(result);
    }

    [HttpPut("{noteId:guid}")]
    [SwaggerOperation(OperationId = "UpdateNote")]
    public async Task<ActionResult<Result<bool>>> Update([FromBody] UpdateNoteRequest request, Guid noteId)
    {
        var result = await mediator.Send(new UpdateNoteCommand(
                NoteId: noteId,
                UserId: UserId,
                Title: request.Title,
                Text: request.Text,
                CategoryId: request.CategoryId,
                NewFiles: request.NewFiles,
                RemovedFiles: request.RemovedFiles
            )
        );

        return Result(result);
    }

    [HttpDelete("{noteId:guid}")]
    [SwaggerOperation(OperationId = "DeleteNote")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid noteId)
    {
        var result = await mediator.Send(new DeleteNoteCommand(
            UserId: UserId,
            NoteId: noteId)
        );

        return Result(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetUserNotesGroupedByCategory")]
    public async Task<ActionResult<Result<GetUserNotesGroupedByCategoryQueryResponse>>>
        GetUserNotesGroupedByCategory()
    {
        var result = await mediator.Send(new GetUserNotesGroupedByCategoryQuery(UserId));
        return Result(result);
    }

    [HttpGet("{noteId:guid}")]
    [SwaggerOperation(OperationId = "GetNoteDetail")]
    public async Task<ActionResult<GetNoteDetailQueryResponse>> GetNoteDetail(Guid noteId)
    {
        var result = await mediator.Send(new GetNoteDetailQuery(
            UserId: UserId,
            NoteId: noteId)
        );
        
        return Result(result);
    }
}