using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetUserCategories;

public class GetUserCategoriesQueryValidator : AbstractValidator<GetUserCategoriesQuery>
{
    public GetUserCategoriesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی تواند خالی باشد.}")
            .WithName("شناسه کاربر");
    }
}