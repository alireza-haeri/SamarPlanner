using FluentValidation;
using SamarPlanner.Shared.Contracts.Queries;

namespace SamarPlanner.Report.Application.UseCases.Queries.GetReportDetail;

public class GetReportDetailQueryValidator : AbstractValidator<GetReportDetailQuery>
{
    public GetReportDetailQueryValidator()
    {
        RuleFor(x => x.ReportId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی گزارش");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} نمی‌تواند خالی باشد.")
            .NotNull().WithMessage("{PropertyName} نمی‌تواند null باشد.")
            .WithName("آیدی کاربر");
    }
}