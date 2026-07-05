using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Note.Application.UseCases.Commands.UpdateNoteCategory;

public class UpdateNoteCategoryCommandValidator : AbstractValidator<UpdateNoteCategoryCommand>
{
    public UpdateNoteCategoryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");

        RuleFor(x => x.NoteCategoryId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه دسته بندی یادداشت");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("عنوان");
    }
}