using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeTaskOccurrenceSkip;

public class ChangeTaskOccurrenceSkipValidator : AbstractValidator<ChangeTaskOccurrenceSkipCommand>
{
    public ChangeTaskOccurrenceSkipValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی وظیفه");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .LessThanOrEqualTo(d => DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("{PropertyName} نمی‌تواند تاریخی در آینده باشد.")
            .WithName("تاریخ");

        RuleFor(x => x.IsSkipped)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("وضعیت رد شدن");
    }
}