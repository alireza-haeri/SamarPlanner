using MediatR;
using SamarPlanner.Note.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetUserCategories;

public class GetUserCategoriesQueryHandler(INoteCategoryRepository noteCategoryRepository)
    : IRequestHandler<GetUserCategoriesQuery, Result<GetUserCategoriesQueryResponse>>
{
    public async Task<Result<GetUserCategoriesQueryResponse>> Handle(GetUserCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await noteCategoryRepository.GetUserCategoryAsync(request.UserId, cancellationToken);
        return Result<GetUserCategoriesQueryResponse>.Success(
            new GetUserCategoriesQueryResponse(categories
                .Select(c =>
                    new GetUserCategoriesQueryResponseCategory(c.Id, c.Title)
                )
                .ToList()
            )
        );
    }
}