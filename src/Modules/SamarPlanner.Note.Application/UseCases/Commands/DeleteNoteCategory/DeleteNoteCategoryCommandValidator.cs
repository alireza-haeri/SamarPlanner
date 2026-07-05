using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Note.Application.UseCases.Commands.DeleteNoteCategory;

public class DeleteNoteCategoryCommandValidator : AbstractValidator<DeleteNoteCategoryCommand>
{
    public DeleteNoteCategoryCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");

        RuleFor(x => x.NoteCategoryId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه دسته بندی یادداشت");
    }
}