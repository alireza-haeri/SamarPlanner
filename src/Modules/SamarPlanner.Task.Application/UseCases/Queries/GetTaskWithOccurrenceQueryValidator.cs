using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Task.Application.UseCases.Queries;

public class GetTaskWithOccurrenceQueryValidator : AbstractValidator<GetTaskWithOccurrencesQuery>
{
    public GetTaskWithOccurrenceQueryValidator()
    {
        RuleFor(x=>x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
        
        RuleFor(x=>x.From)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ شروع");
        
        RuleFor(x=>x.To)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .GreaterThanOrEqualTo(x=>x.From)
            .WithMessage("{PropertyName} باید بزرگتر یا مساوی {ComparisonValue} باشد.")
            .WithName("تاریخ پایان");
    }
}