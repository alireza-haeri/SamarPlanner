using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetUserNotesGroupedByCategory;

public class GetUserNotesGroupedByCategoryQueryValidator : AbstractValidator<GetUserNotesGroupedByCategoryQuery>
{
    public GetUserNotesGroupedByCategoryQueryValidator()
    {
        RuleFor(x=>x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");
    }
}