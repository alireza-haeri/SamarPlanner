using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Identity.Contract;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Identity.Controllers;

[ApiController]
[Tags("Identity")]
[Route("api/v1/identity")]
public class IdentityController(IMediator mediator) : BaseController
{
    [HttpPost("authentication")]
    [SwaggerOperation(OperationId = "RegisterOrLogin")]
    public async Task<ActionResult<Result<RegisterOrLoginCommandResponse>>> RegisterOrLogin([FromBody]RegisterOrLoginRequest request, CancellationToken cancellationToken)
    {
        var result =
            await mediator.Send(new RegisterOrLoginCommand(request.PhoneNumber, request.Password), cancellationToken);
        return Result(result);
    }
}