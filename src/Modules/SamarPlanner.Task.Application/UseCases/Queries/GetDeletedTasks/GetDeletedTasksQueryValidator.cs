using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Task.Application.UseCases.Queries.GetDeletedTasks;

public class GetDeletedTasksQueryValidator : AbstractValidator<GetDeletedTasksQuery>
{
    public GetDeletedTasksQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
    }
}