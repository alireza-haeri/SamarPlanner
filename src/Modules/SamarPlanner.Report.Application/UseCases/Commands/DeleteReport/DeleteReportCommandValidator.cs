using FluentValidation;
using SamarPlanner.Shared.Contracts.Command;

namespace SamarPlanner.Report.Application.UseCases.Commands.DeleteReport;

public class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
{
    public DeleteReportCommandValidator()
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