using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Goal.Contracts;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Goal.Controllers;

[Authorize]
[Route("api/v1/goals")]
[ApiController]
public class GoalController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateGoal")]
    public async Task<IActionResult> Create([FromBody] CreateGoalRequest request)
    {
        var result = await mediator.Send(new CreateGoalCommand(
                UserId,
                Title: request.Title,
                Description: request.Description,
                Priority: request.Priority,
                GoalType: request.GoalType,
                PeriodStart: request.PeriodStart,
                PeriodEnd: request.PeriodEnd,
                ParentGoalId: request.ParentGoalId)
            );
        
        return Result(result);
    }

    [HttpPut]
    [SwaggerOperation(OperationId = "UpdateGoal")]
    public async Task<IActionResult> Update([FromBody] UpdateGoalRequest request)
    {
        var result = await mediator.Send(new UpdateGoalCommand(
            request.GoalId,
            UserId,
            request.Title,
            request.Description,
            request.Priority,
            request.GoalType,
            request.PeriodStart,
            request.PeriodEnd,
            request.ParentGoalId
        ));
        
        return Result(result);
    }

    [HttpDelete("{goalId:guid}")]
    [SwaggerOperation(OperationId = "DeleteGoal")]
    public async Task<IActionResult> Delete(Guid goalId)
    {
        var result = await mediator.Send(new DeleteGoalCommand(goalId, UserId));
        return Result(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetGoals")]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllGoalsByUserIdQuery(UserId));
        return Result(result);
    }
}