using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Task.Controllers;

[Authorize]
[Route("/api/v1/tasks")]
public class TaskController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateTask")] 
    public async Task<ActionResult<Result<CreateTaskCommandResponse>>> Create([FromBody] CreateTaskRequest request)
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
    [SwaggerOperation(OperationId = "UpdateTask")]
    public async Task<ActionResult<Result<bool>>> Update(Guid taskId, [FromBody] UpdateTaskRequest request)
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
    [SwaggerOperation(OperationId = "DeleteTask")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid taskId)
    {
        var result = await mediator.Send(new DeleteTaskCommand(
            TaskId: taskId,
            UserId: UserId)
        );
        return Result(result);
    }

    [HttpPut("{taskId:guid}/soft-delete")]
    [SwaggerOperation(OperationId = "SoftDeleteTask")]
    public async Task<ActionResult<Result<bool>>> SoftDelete(Guid taskId)
    {
        var result = await mediator.Send(new SoftDeleteTaskCommand(
            TaskId: taskId,
            UserId: UserId)
        );
        return Result(result);
    }

    [HttpPut("{taskId:guid}/restore")]
    [SwaggerOperation(OperationId = "RestoreTask")]
    public async Task<ActionResult<Result<bool>>> Restore(Guid taskId)
    {
        var result = await mediator.Send(new RestoreTaskCommand(
            TaskId: taskId,
            UserId: UserId)
        );
        return Result(result);
    }

    [HttpGet("with-occurrences")] 
    [SwaggerOperation(OperationId = "GetTasksWithOccurrences")]
    public async Task<ActionResult<Result<GetTaskWithOccurrencesQueryResult>>> GetTaskWithOccurrences([FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        var result = await mediator.Send(new GetTaskWithOccurrencesQuery(
            UserId,
            From: from,
            To: to
        ));
        return Result(result);
    }
}