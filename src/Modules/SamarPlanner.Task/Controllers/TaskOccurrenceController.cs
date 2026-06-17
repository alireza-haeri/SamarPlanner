using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Task.Contracts;

namespace SamarPlanner.Task.Controllers;

[Authorize]
[Route("/api/v1/task-occurrence")]
public class TaskOccurrenceController(IMediator mediator) : BaseController
{
    [HttpPatch("{taskId:guid}/date")]
    public async Task<IActionResult> ChangeDate(Guid taskId, [FromBody] ChangeOccurrenceDateRequest request)
    {
        var result = await mediator.Send(new ChangeOccurrenceDateCommand(
            TaskId: taskId,
            UserId: UserId,
            OldDate: request.OldDate,
            NewDate: request.NewDate
        ));

        return Result(result);
    }

    [HttpPatch("{taskId:guid}/time")]
    public async Task<IActionResult> ChangeTime(Guid taskId, [FromBody] ChangeOccurrenceTimeRequest request)
    {
        var result = await mediator.Send(new ChangeOccurrenceTimeCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date,
            NewTime: request.NewTime
        ));

        return Result(result);
    }

    [HttpPatch("{taskId:guid}/skip")]
    public async Task<IActionResult> ChangeSkip(Guid taskId, [FromBody] SkipOccurrenceRequest request)
    {
        var result = await mediator.Send(new ChangeTaskOccurrenceSkipCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date,
            IsSkipped: request.IsSkipped
        ));

        return Result(result);
    }
    
    [HttpPatch("{taskId:guid}/status")]
    public async Task<IActionResult> ChangeStatus(Guid taskId, [FromBody] ChangeOccurrenceStatusRequest request)
    {
        var result = await mediator.Send(new ChangeTaskOccurrenceStatusCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date,
            Status: request.Status,
            Score: request.Score
        ));

        return Result(result);
    }
    
    [HttpPut("{taskId:guid}/soft-delete")]
    public async Task<IActionResult> SoftDelete(Guid taskId, [FromBody] SoftDeleteOccurrenceRequest request)
    {
        var result = await mediator.Send(new SoftDeleteTaskOccurrenceCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date
        ));

        return Result(result);
    }
    
    [HttpPut("{taskId:guid}/restore")]
    public async Task<IActionResult> Restore(Guid taskId, [FromBody] RestoreTaskOccurrenceRequest request)
    {
        var result = await mediator.Send(new RestoreTaskOccurrenceCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date
        ));

        return Result(result);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete(Guid taskId,DeleteTaskOccurrenceRequest request)
    {
        var result = await mediator.Send(new DeleteTaskOccurrenceCommand(
            TaskId: taskId,
            UserId: UserId,
            Date: request.Date
        ));
        
        return Result(result);
    }
}