using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Report.Controllers;

[Authorize]
[Route("/api/v1/highlights")]
public class HighlightController(IMediator mediator) : BaseController
{
    [HttpGet]
    [SwaggerOperation(OperationId = "GetHighlightsSuggestions")]
    public async Task<ActionResult<Result<GetHighlightSuggestionsQueryResponse>>> GetHighlightsSuggestions(
        [FromQuery(Name = "text")] string text)
    {
        var result = await mediator.Send(new GetHighlightSuggestionsQuery(
            UserId: UserId,
            Text: text
        ));

        return Result(result);
    }
}