using FluentValidation;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeOccurrenceDateCommand;

public class ChangeOccurrenceDateCommandValidator : AbstractValidator<Shared.Contracts.Command.ChangeOccurrenceDateCommand>
{
    public ChangeOccurrenceDateCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی وظیفه");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");

        RuleFor(x => x.OldDate)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ قدیمی");

        RuleFor(x => x.NewDate)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ جدید");
    }
}