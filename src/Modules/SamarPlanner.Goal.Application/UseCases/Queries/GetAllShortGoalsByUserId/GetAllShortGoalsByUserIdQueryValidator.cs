using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Goal.Application.UseCases.Queries.GetAllShortGoalsByUserId;

public class GetAllShortGoalsByUserIdQueryValidator : AbstractValidator<GetAllShortGoalsByUserIdQuery>
{
    public GetAllShortGoalsByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("شناسه کاربر");

    }
}