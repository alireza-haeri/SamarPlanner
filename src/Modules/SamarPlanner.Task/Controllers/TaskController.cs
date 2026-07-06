using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using SamarPlanner.Task.Contracts;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Task.Controllers;

[Authorize]
[Tags("Task")]
[Route("/api/v1/tasks")]
public class TaskController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateTask")] 
    public async Task<ActionResult<Result<CreateTaskCommandResponse>>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
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
        ), cancellationToken);
        return Result(result);
    }

    [HttpPut("{taskId:guid}")]
    [SwaggerOperation(OperationId = "UpdateTask")]
    public async Task<ActionResult<Result<bool>>> Update(Guid taskId, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
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
        ), cancellationToken);
        return Result(result);
    }

    [HttpDelete("{taskId:guid}")]
    [SwaggerOperation(OperationId = "DeleteTask")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid taskId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteTaskCommand(
            TaskId: taskId,
            UserId: UserId), cancellationToken);
        return Result(result);
    }

    [HttpPut("{taskId:guid}/soft-delete")]
    [SwaggerOperation(OperationId = "SoftDeleteTask")]
    public async Task<ActionResult<Result<bool>>> SoftDelete(Guid taskId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new SoftDeleteTaskCommand(
            TaskId: taskId,
            UserId: UserId), cancellationToken);
        return Result(result);
    }

    [HttpPut("{taskId:guid}/restore")]
    [SwaggerOperation(OperationId = "RestoreTask")]
    public async Task<ActionResult<Result<bool>>> Restore(Guid taskId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RestoreTaskCommand(
            TaskId: taskId,
            UserId: UserId), cancellationToken);
        return Result(result);
    }

    [HttpGet("with-occurrences")] 
    [SwaggerOperation(OperationId = "GetTasksWithOccurrences")]
    public async Task<ActionResult<Result<GetTasksWithOccurrencesQueryResult>>> GetTasksWithOccurrences([FromQuery] DateOnly from, [FromQuery] DateOnly to, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTasksWithOccurrencesQuery(
            UserId,
            From: from,
            To: to
        ), cancellationToken);
        return Result(result);
    }

    [HttpGet("{taskId:guid}")] 
    [SwaggerOperation(OperationId = "GetTaskDetail")]
    public async Task<ActionResult<Result<GetTaskDetailQueryResult>>> GetTaskDetail(Guid taskId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTaskDetailQuery(
            TaskId:taskId,
           UserId: UserId
        ), cancellationToken);
        return Result(result);
    }

    [HttpGet("/deleted")] 
    [SwaggerOperation(OperationId = "GetDeletedTasks")]
    public async Task<ActionResult<Result<GetDeletedTasksQueryResult>>> GetDeletedTasks(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetDeletedTasksQuery(
            UserId: UserId
        ), cancellationToken);
        return Result(result);
    }
}