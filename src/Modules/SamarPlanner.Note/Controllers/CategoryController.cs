using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Note.Contracts;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;
using Swashbuckle.AspNetCore.Annotations;

namespace SamarPlanner.Note.Controllers;

[Authorize]
[Tags("Note")]
[Route("/api/v1/categories")]
public class CategoryController(IMediator mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(OperationId = "CreateCategory")]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateNoteCategoryCommand(
            UserId: UserId,
            Title: request.Title), cancellationToken);
        return Result(result);
    }

    [HttpPut("{categoryId:guid}")]
    [SwaggerOperation(OperationId = "UpdateCategory")]
    public async Task<ActionResult<Result<bool>>> Update([FromBody] UpdateCategoryRequest request, Guid categoryId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateNoteCategoryCommand(
                UserId: UserId,
                NoteCategoryId: categoryId,
                Title: request.Title
            ), cancellationToken);

        return Result(result);
    }

    [HttpDelete("{categoryId:guid}")]
    [SwaggerOperation(OperationId = "DeleteCategory")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid categoryId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteNoteCategoryCommand(
            UserId: UserId,
            NoteCategoryId: categoryId), cancellationToken);

        return Result(result);
    }

    [HttpGet]
    [SwaggerOperation(OperationId = "GetUserCategories")]
    public async Task<ActionResult<Result<GetUserCategoriesQueryResponse>>> GetUserCategories(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserCategoriesQuery(
            UserId: UserId), cancellationToken);
        return Result(result);
    }
}
