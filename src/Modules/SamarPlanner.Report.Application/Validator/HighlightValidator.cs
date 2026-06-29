using FluentValidation;
using SamarPlanner.Shared.Contracts.Contracts;

namespace SamarPlanner.Report.Application.Validator;

public class HighlightValidator : AbstractValidator<ReportHighlightDto>
{
    public HighlightValidator()
    {
        RuleFor(x=>x.Text)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .MaximumLength(1000).WithMessage("{PropertyName} حداکثر 1000 کاراکتر می‌تواند باشد.")
            .WithName("متن");
        
        RuleFor(x=>x.Type)
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .IsInEnum().WithMessage("{PropertyName} مقدار معتبری نیست.")
            .WithName("نوع");
    }
}