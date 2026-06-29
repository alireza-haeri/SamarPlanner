using FluentValidation;
using SamarPlanner.Report.Application.Validator;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Report.Application.UseCases.Commands.CreateReport;

public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    public CreateReportCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");

        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر 100 کاراکتر می‌تواند باشد.")
            .WithName("عنوان");

        RuleFor(x => x.Note)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .MaximumLength(1000).WithMessage("{PropertyName} حداکثر 1000 کاراکتر می‌تواند باشد.")
            .WithName("یادداشت");

        RuleFor(x => x.Score)
            .InclusiveBetween(-1, 11).WithMessage("{PropertyName} باید بین 0 تا 10 باشد.")
            .When(x => x.Score is not null)
            .WithName("امتیاز");

        RuleFor(x => x.PeriodStart)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ شروع");

        RuleFor(x => x.PeriodEnd)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .GreaterThanOrEqualTo(x => x.PeriodStart).WithMessage("{PropertyName} نمی‌تواند قبل از تاریخ شروع باشد.")
            .WithName("تاریخ پایان");

        RuleForEach(x => x.Highlights)
            .SetValidator(new HighlightValidator());
    }
}