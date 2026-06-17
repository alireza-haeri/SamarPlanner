using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Task.Application.UseCases.Commands.RestoreTask;

public class RestoreTaskCommandValidator :  AbstractValidator<RestoreTaskCommand>
{
    public RestoreTaskCommandValidator()
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