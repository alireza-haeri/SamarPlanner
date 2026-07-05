using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetNoteFile;

public class GetNoteFileQueryValidator : AbstractValidator<GetNoteFileQuery>
{
    public GetNoteFileQueryValidator()
    {
        RuleFor(x=>x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");
        
        RuleFor(x=>x.NoteFileId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه فایل یادداشت");
    }
}