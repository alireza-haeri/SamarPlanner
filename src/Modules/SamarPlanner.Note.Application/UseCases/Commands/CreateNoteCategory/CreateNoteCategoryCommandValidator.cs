using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Note.Application.UseCases.Commands.CreateNoteCategory;

public class CreateNoteCategoryCommandValidator : AbstractValidator<CreateNoteCategoryCommand>
{
    public CreateNoteCategoryCommandValidator()
    {
        RuleFor(x=>x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");
        
        RuleFor(x=>x.Title)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("عنوان");
    }
}