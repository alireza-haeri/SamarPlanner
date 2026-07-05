using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Note.Application.UseCases.Queries.GetNoteDetail;

public class GetNoteDetailQueryValidator : AbstractValidator<GetNoteDetailQuery>
{
    public GetNoteDetailQueryValidator()
    {
        RuleFor(x=>x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه کاربر");
        
        RuleFor(x=>x.NoteId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .WithName("شناسه یادداشت");
    }
}