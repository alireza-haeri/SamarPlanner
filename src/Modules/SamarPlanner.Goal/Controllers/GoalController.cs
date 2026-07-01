using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Goal.Contracts;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Goal.Controllers;

[Authorize]
[Route("api/v1/goals")]
[ApiController]
public class GoalController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateGoal")]
    public async Task<ActionResult<Result<CreateGoalCommandResponse>>> Create([FromBody] CreateGoalRequest request)
    {
        var result = await mediator.Send(new CreateGoalCommand(
                UserId,
                Title: request.Title,
                Description: request.Description,
                Priority: request.Priority,
                PeriodStart: request.PeriodStart,
                PeriodEnd: request.PeriodEnd,
                ParentGoalId: request.ParentGoalId)
            );
        
        return Result(result);
    }

    [HttpPut]
    [SwaggerOperation(OperationId = "UpdateGoal")]
    public async Task<ActionResult<Result<bool>>> Update([FromBody] UpdateGoalRequest request)
    {
        var result = await mediator.Send(new UpdateGoalCommand(
            request.GoalId,
            UserId,
            request.Title,
            request.Description,
            request.Priority,
            request.PeriodStart,
            request.PeriodEnd,
            request.ParentGoalId
        ));
        
        return Result(result);
    }

    [HttpPatch("{goalId:guid}/achieve")]
    [SwaggerOperation(OperationId = "AchieveGoal")]
    public async Task<ActionResult<Result<bool>>> AchieveGaol(Guid  goalId)
    {
        var result = await mediator.Send(new AchieveGoalCommand(
            UserId: UserId,
            GoalId: goalId));
        
        return Result(result);
    }

    [HttpPatch("{goalId:guid}/active")]
    [SwaggerOperation(OperationId = "ActiveGoal")]
    public async Task<ActionResult<Result<bool>>> ActiveGaol(Guid  goalId)
    {
        var result = await mediator.Send(new ActiveGoalCommand(
            UserId: UserId,
            GoalId: goalId));
        
        return Result(result);
    }

    [HttpPatch("{goalId:guid}/fail")]
    [SwaggerOperation(OperationId = "FailGoal")]
    public async Task<ActionResult<Result<bool>>> FailGaol(Guid  goalId)
    {
        var result = await mediator.Send(new FailGoalCommand(
            UserId: UserId,
            GoalId: goalId));
        
        return Result(result);
    }

    [HttpPatch("{goalId:guid}/rollover")]
    [SwaggerOperation(OperationId = "RolloverGoal")]
    public async Task<ActionResult<Result<RolloverGoalCommandResult>>> RolloverGaol(Guid  goalId, RolloverGoalRequest request)
    {
        var result = await mediator.Send(new RolloverGoalCommand(
            UserId: UserId,
            GoalId: goalId,
            PeriodStart: request.PeriodStart,
            PeriodEnd: request.PeriodEnd
        ));
        
        return Result(result);
    }
    
    [HttpDelete("{goalId:guid}")]
    [SwaggerOperation(OperationId = "DeleteGoal")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid goalId)
    {
        var result = await mediator.Send(new DeleteGoalCommand(goalId, UserId));
        return Result(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetGoals")]
    public async Task<ActionResult<Result<GetAllGoalsByUserIdQueryResponse>>> GetAll()
    {
        var result = await mediator.Send(new GetAllGoalsByUserIdQuery(UserId));
        return Result(result);
    }
    
    [HttpGet("short")]
    [SwaggerOperation(OperationId = "GetShortGoals")]
    public async Task<ActionResult<Result<GetAllShortGoalsByUserIdQueryResponse>>> GetAllShortGoals()
    {
        var result = await mediator.Send(new GetAllShortGoalsByUserIdQuery(UserId));
        return Result(result);
    }
}