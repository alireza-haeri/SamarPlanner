using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SamarPlanner.Shared.Kernel;
using Microsoft.Extensions.Logging;

namespace SamarPlanner.Shared;

public class BaseController : ControllerBase
{
    protected ActionResult Result<TResponse>(Result<TResponse> result)
    {
        if (result.IsSuccess)
            return Ok(result);

        return result.BadResultType switch
        {
            BadResultType.NotFound => NotFound(result),
            BadResultType.Validation => BadRequest(result),
            BadResultType.General => StatusCode(500, result),
            _ => StatusCode(500, new { error = "Unknown bad result type" })
        };
    }
}