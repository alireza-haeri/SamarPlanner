using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Report.Application.UseCases.Queries.GetHighlightSuggestions;

public class GetHighlightSuggestionsQueryValidator : AbstractValidator<GetHighlightSuggestionsQuery>
{
    public GetHighlightSuggestionsQueryValidator()
    {
        RuleFor(x=>x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
        
        RuleFor(x=>x.Text)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .MinimumLength(3).WithMessage("{PropertyName} حداقل 3 کاراکتر می‌تواند باشد.")
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر 100 کاراکتر می‌تواند باشد.")
            .WithName("متن");
    }
}