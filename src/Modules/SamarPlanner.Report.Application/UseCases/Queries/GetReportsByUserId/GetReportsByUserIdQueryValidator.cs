using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Report.Application.UseCases.Queries.GetReportsByUserId;

public class GetReportsByUserIdQueryValidator : AbstractValidator<GetReportsByUserIdQuery>
{
    public GetReportsByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
        
    }
}