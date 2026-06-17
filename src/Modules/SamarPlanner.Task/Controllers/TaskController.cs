using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Task.Contracts;

namespace SamarPlanner.Task.Controllers;

[Authorize]
[Route("/api/v1/task")]
public class TaskController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        var result = await mediator.Send(new CreateTaskCommand(
            UserId: UserId,
            Title: request.Title,
            Description: request.Description,
            Date: request.Date,
            DefaultTime: request.DefaultTime,
            Priority: request.Priority,
            Type: request.Type,
            RepeatPattern: request.RepeatPattern,
            ParentGoalId: request.ParentGoalId
        ));

        return Result(result);
    }

    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> Update(Guid taskId, [FromBody] UpdateTaskRequest request)
    {
        var result = await mediator.Send(new UpdateTaskCommand(
            TaskId: taskId,
            UserId: UserId,
            Title: request.Title,
            Description: request.Description,
            Date: request.Date,
            DefaultTime: request.DefaultTime,
            Priority: request.Priority,
            Type: request.Type,
            RepeatPattern: request.RepeatPattern,
            ParentGoalId: request.ParentGoalId
        ));

        return Result(result);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete(Guid taskId)
    {
        var result = await mediator.Send(new DeleteTaskCommand(
            TaskId: taskId,
            UserId: UserId)
        );

        return Result(result);
    }

    [HttpPut("{taskId:guid}/soft-delete")]
    public async Task<IActionResult> SoftDelete(Guid taskId)
    {
        var result = await mediator.Send(new SoftDeleteTaskCommand(
            TaskId: taskId,
            UserId: UserId)
        );

        return Result(result);
    }

    [HttpPut("{taskId:guid}/restore")]
    public async Task<IActionResult> Restore(Guid taskId)
    {
        var result = await mediator.Send(new RestoreTaskCommand(
            TaskId: taskId,
            UserId: UserId)
        );

        return Result(result);
    }

    [HttpGet("{from}/{to}")]
    public async Task<IActionResult> GetTaskWithOccurrencesQuery(DateOnly from, DateOnly to)
    {
        var result = await mediator.Send(new GetTaskWithOccurrencesQuery(
            UserId,
            From: from,
            To: to
        ));

        return Result(result);
    }
}