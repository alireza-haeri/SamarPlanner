using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Task.Controllers;

[Authorize]
[Tags("Task")]
[Route("/api/v1/task-occurrences")]
public class TaskOccurrenceController(IMediator mediator) : BaseController
{
    [HttpPatch("{taskId:guid}/date")]
    [SwaggerOperation(OperationId = "ChangeOccurrenceDate")]
    public async Task<ActionResult<Result<bool>>> ChangeDate(Guid taskId, [FromBody] ChangeOccurrenceDateRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ChangeOccurrenceDateCommand(
            TaskId: taskId,
            UserId: UserId,
            OldDate: request.OldDate,
            NewDate: request.NewDate
        ), cancellationToken);
        return Result(result);
    }

    [HttpPatch("{taskId:guid}/time")]
    [SwaggerOperation(OperationId = "ChangeOccurrenceTime")]
    public async Task<ActionResult<Result<bool>>> ChangeTime(Guid taskId, [FromBody] ChangeOccurrenceTimeRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ChangeOccurrenceTimeCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date,
            NewTime: request.NewTime
        ), cancellationToken);
        return Result(result);
    }

    [HttpPatch("{taskId:guid}/skip")]
    [SwaggerOperation(OperationId = "SkipOccurrence")]
    public async Task<ActionResult<Result<bool>>> ChangeSkip(Guid taskId, [FromBody] SkipOccurrenceRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ChangeTaskOccurrenceSkipCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date,
            IsSkipped: request.IsSkipped
        ), cancellationToken);
        return Result(result);
    }

    [HttpPatch("{taskId:guid}/status")]
    [SwaggerOperation(OperationId = "ChangeOccurrenceStatus")]
    public async Task<ActionResult<Result<bool>>> ChangeStatus(Guid taskId, [FromBody] ChangeOccurrenceStatusRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ChangeTaskOccurrenceStatusCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date,
            Status: request.Status,
            Score: request.Score
        ), cancellationToken);
        return Result(result);
    }
}