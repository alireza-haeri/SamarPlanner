using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Task.Application.UseCases.Commands.SoftDeleteOccurrenceTask;

public class SoftDeleteTaskOccurrenceCommandValidator : AbstractValidator<SoftDeleteTaskOccurrenceCommand>
{
    public SoftDeleteTaskOccurrenceCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی وظیفه");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
        
        RuleFor(x=>x.Date)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ");
    }
}