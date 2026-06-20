using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Task.Application.UseCases.Commands.ChangeTaskOccurrenceStatus;

public class ChangeTaskOccurrenceStatusCommandValidator : AbstractValidator<ChangeTaskOccurrenceStatusCommand>
{
    public ChangeTaskOccurrenceStatusCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی وظیفه");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .IsInEnum().WithMessage("{PropertyName} مقدار معتبری نیست.")
            .WithName("وضعیت");

        RuleFor(x => x.Score)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .ExclusiveBetween(-1, 11).WithMessage("{PropertyName} باید بین 0 و 10 باشد.")
            .When(x => x.Score != null)
            .WithName("امتیاز");

        RuleFor(x => x.Score)
            .Must(x => x is null)
            .When(x => x.Status != Core.Enums.TaskStatus.AlmostDone)
            .WithMessage("{PropertyName} باید null باشد زمانی که وضعیت وظیفه AlmostDone نیست.")
            .WithName("امتیاز");
        
        RuleFor(x=>x.Date)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .LessThanOrEqualTo(d => DateOnly.FromDateTime(DateTime.Now))
            .WithName("تاریخ");
    }
}