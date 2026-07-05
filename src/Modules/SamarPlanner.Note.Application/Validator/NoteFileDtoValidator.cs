using FluentValidation;
using SamarPlanner.Note.Core.Contracts;
using SamarPlanner.Note.Core.Entities;
using SamarPlanner.Shared.Contracts.Contracts;

namespace SamarPlanner.Note.Application.Validator;

public class NoteFileDtoValidator : AbstractValidator<NoteFileDto>
{
    public NoteFileDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100).WithMessage("{PropertyName} حداکثر {MaxLength} کاراکتر می‌تواند باشد.")
            .WithName("عنوان فایل");

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .Must(ct => AllowedFileTypes.ContentTypeToExtension.ContainsKey(ct))
            .WithMessage("نوع فایل مجاز نیست.")
            .WithName("نوع محتوا");

        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage("{PropertyName} باید بزرگتر از صفر باشد.")
            .LessThanOrEqualTo(NoteFile.MaxFileSizeBytes)
            .WithMessage("{PropertyName} نمی‌تواند بزرگتر از 5 مگابایت باشد.")
            .WithName("اندازه فایل");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("محتوای فایل");

        RuleFor(x => x)
            .Must(x => x.Content.Length == x.Length)
            .WithMessage("اندازه‌ی اعلام‌شده با حجم واقعی فایل مطابقت ندارد.");
    }
}