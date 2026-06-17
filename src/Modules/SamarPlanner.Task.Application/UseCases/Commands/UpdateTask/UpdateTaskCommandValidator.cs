using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Task.Application.Validators;

namespace SamarPlanner.Task.Application.UseCases.Commands.UpdateTask;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی وظیفه");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر 100 کاراکتر می‌تواند باشد.")
            .WithName("عنوان");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("{PropertyName} حداکثر 1000 کاراکتر می‌تواند باشد.")
            .WithName("توضیحات");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("تاریخ");

        RuleFor(x => x.DefaultTime)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .When(x => x.DefaultTime is not null)
            .WithName("ساعت");
        
        RuleFor(x => x.RepeatPattern)
            .SetValidator(new CreateTaskRepeatPatternDtoValidator());

        RuleFor(x => x.Priority)
            .IsInEnum().When(x => x.Priority is not null)
            .WithMessage("{PropertyName} مقدار معتبری نیست.")
            .WithName("اولویت");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .IsInEnum().WithMessage("{PropertyName} مقدار معتبری نیست.")
            .WithName("نوع");

        RuleFor(x => x.ParentGoalId);
    }
}