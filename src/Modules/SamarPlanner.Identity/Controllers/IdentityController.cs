using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Identity.Contract;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Identity.Controllers;

[ApiController]
[Route("api/v1/identity")]
public class IdentityController(IMediator mediator) : BaseController
{
    [HttpPost("authentication")]
    public async Task<IActionResult> RegisterOrLogin([FromBody]RegisterOrLoginRequest request)
    {
        var result =
            await mediator.Send(new RegisterOrLoginCommand(request.PhoneNumber, request.Password));
        return Result(result);
    }
    
    [Authorize]
    [HttpGet("/test")]
    public IActionResult Test()
    {
        return Ok();
    }
}