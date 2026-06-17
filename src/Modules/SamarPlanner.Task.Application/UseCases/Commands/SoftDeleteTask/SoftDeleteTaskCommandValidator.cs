using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Task.Application.UseCases.Commands.SoftDeleteTask;

public class SoftDeleteTaskCommandValidator :  AbstractValidator<SoftDeleteTaskCommand>
{
    public SoftDeleteTaskCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی وظیفه");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");

    }
}