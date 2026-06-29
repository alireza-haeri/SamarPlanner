using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamarPlanner.Report.Contracts;
using SamarPlanner.Shared;
using SamarPlanner.Shared.Contracts.Command;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Controllers;

[Authorize]
[Route("/api/v1/reports")]
public class ReportController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Result<Guid>>> Create([FromBody] CreateReportRequest request)
    {
        var result = await mediator.Send(new CreateReportCommand(
            UserId: UserId,
            Title: request.Title,
            Note: request.Note,
            Score: request.Score,
            PeriodStart: request.PeriodStart,
            PeriodEnd: request.PeriodEnd,
            Highlights: request.Highlights)
        );

        return Result(result);
    }

    [HttpPut("{reportId:guid}")]
    public async Task<ActionResult<Result<bool>>> Update([FromBody] UpdateReportRequest request, Guid reportId)
    {
        var result = await mediator.Send(new UpdateReportCommand(
            ReportId: reportId,
            UserId: UserId,
            Title: request.Title,
            Note: request.Note,
            Score: request.Score,
            PeriodStart: request.PeriodStart,
            PeriodEnd: request.PeriodEnd,
            Highlights: request.Highlights)
        );

        return Result(result);
    }

    [HttpDelete("{reportId:guid}")]
    public async Task<ActionResult<Result<bool>>> Delete(Guid reportId)
    {
        var result = await mediator.Send(new DeleteReportCommand(
            ReportId: reportId,
            UserId: UserId)
        );

        return Result(result);
    }

    [HttpGet]
    public async Task<ActionResult<Result<GetReportsByUserIdQueryResponse>>> GetReports(
        [FromQuery(Name = "periodStart")] DateOnly periodStart, [FromQuery(Name = "periodEnd")] DateOnly periodEnd)
    {
        var result = await mediator.Send(new GetReportsByUserIdQuery(
            UserId: UserId,
            PeriodStart: periodStart,
            PeriodEnd: periodEnd)
        );

        return Result(result);
    }

    [HttpGet("{reportId:guid}")]
    public async Task<ActionResult<Result<GetReportDetailQueryResponse>>> GetReportDetail(Guid reportId)
    {
        var result = await mediator.Send(new GetReportDetailQuery(
            ReportId: reportId,
            UserId: UserId)
        );

        return Result(result);
    }
}