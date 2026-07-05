using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Note.Application.UseCases.Commands.DeleteNote;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");
        
        RuleFor(v => v.NoteId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه یادداشت");
    }
}