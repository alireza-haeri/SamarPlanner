using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Note.Contracts;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Note.Controllers;

[Authorize]
[Route("/api/v1/categories")]
public class CategoryController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateCategory")]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateCategoryRequest request)
    {
        var result = await mediator.Send(new CreateNoteCategoryCommand(
            UserId: UserId,
            Title: request.Title)
        );
        return Result(result);
    }

    [HttpPut("{categoryId:guid}")]
    [SwaggerOperation(OperationId = "UpdateCategory")]
    public async Task<ActionResult<Result<bool>>> Update([FromBody] UpdateCategoryRequest request, Guid categoryId)
    {
        var result = await mediator.Send(new UpdateNoteCategoryCommand(
                UserId: UserId,
                NoteCategoryId: categoryId,
                Title: request.Title
            )
        );

        return Result(result);
    }

    [HttpDelete("{categoryId:guid}")]
    [SwaggerOperation(OperationId = "DeleteCategory")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid categoryId)
    {
        var result = await mediator.Send(new DeleteNoteCategoryCommand(
            UserId: UserId,
            NoteCategoryId: categoryId)
        );

        return Result(result);
    }
}
