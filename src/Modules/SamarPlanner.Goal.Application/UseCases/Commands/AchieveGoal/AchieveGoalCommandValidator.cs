using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Goal.Application.UseCases.Commands.AchieveGoal;

public class AchieveGoalCommandValidator : AbstractValidator<AchieveGoalCommand>
{
    public AchieveGoalCommandValidator()
    {
        RuleFor(x => x.GoalId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی هدف");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
    }
}