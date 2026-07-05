using FluentValidation;
using SamarPlanner.Note.Application.Validator;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Note.Application.UseCases.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");
        
        RuleFor(x => x.NoteId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه یادداشت");

        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("عنوان");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .MaximumLength(1000).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("متن");

        RuleForEach(x => x.NewFiles)
            .SetValidator(new NoteFileDtoValidator())
            .WithName("فایل‌ها");

        RuleForEach(x => x.RemovedFiles)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .When(x => x.RemovedFiles.Any())
            .WithName("فایل‌های حذف شده");
    }
}