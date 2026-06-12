using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Goal.Application.UseCases.Commands.CreateGoal;

public class CreateGoalCommandValidator : AbstractValidator<CreateGoalCommand>
{
    public CreateGoalCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("شناسه کاربر");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("عنوان");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("توضیحات");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .IsInEnum().WithMessage("{PropertyName} مقدار معتبری ندارد.")
            .WithName("اولویت");

        RuleFor(x => x.GoalType)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .IsInEnum().WithMessage("{PropertyName} مقدار معتبری ندارد.")
            .WithName("نوع هدف");

        RuleFor(x => x.PeriodStart)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("زمان آغاز");

        RuleFor(x => x.PeriodEnd)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .Must((model, periodEnd) => periodEnd > model.PeriodStart)
            .WithMessage("زمان پایان باید بعد از زمان آغاز باشد.");

        RuleFor(x => x.ParentGoalId);
    }
}