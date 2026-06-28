using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Goal.Application.UseCases.Commands.RolloverGoal;

public class RolloverGoalCommandValidator : AbstractValidator<RolloverGoalCommand>
{
    public RolloverGoalCommandValidator()
    {
        RuleFor(x => x.GoalId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی هدف");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
        
        RuleFor(x=>x.PeriodStart)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ شروع دوره");
        
        RuleFor(x => x.PeriodEnd)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ پایان دوره");
    }
}