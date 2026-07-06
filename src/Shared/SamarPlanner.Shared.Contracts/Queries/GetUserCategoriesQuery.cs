using MediatR;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetUserCategoriesQuery(Guid UserId) : IRequest<Result<GetUserCategoriesQueryResponse>>;

public record GetUserCategoriesQueryResponse(List<GetUserCategoriesQueryResponseCategory> Categories);
public record GetUserCategoriesQueryResponseCategory(Guid Id, string Title);