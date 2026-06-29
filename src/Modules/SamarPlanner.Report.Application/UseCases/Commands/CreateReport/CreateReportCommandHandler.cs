using MediatR;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Report.Core.Entities;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Application.UseCases.Commands.CreateReport;

public class CreateReportCommandHandler(IReportRepository reportRepository)
    : IRequestHandler<CreateReportCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var highlights = request.Highlights
            .Select(h => ReportHighlight.Create(h.Text, h.Type))
            .ToList();

        var report = Core.Entities.Report.Create(request.Title, request.Note, request.Score, request.PeriodStart,
            request.PeriodEnd, highlights, request.UserId);

        var createResult = await reportRepository.CreateAsync(report, cancellationToken);
        if (createResult is null)
            return Result<Guid>.GeneralFailure("خطا در ایجاد گزارش");

        return Result<Guid>.Success(report.Id);
    }
}