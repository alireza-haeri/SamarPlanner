using FluentValidation;
using SamarPlanner.Note.Application.Validator;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Note.Application.UseCases.Commands.CreateNote;

public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("شناسه کاربر");
        
        RuleFor(x=>x.Title)
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("عنوان");
        
        RuleFor(x=>x.Text)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .MaximumLength(1000).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("متن");
        
      RuleForEach(x=>x.Files)
          .SetValidator(new NoteFileDtoValidator())
          .WithName("فایل‌ها");
    }
}