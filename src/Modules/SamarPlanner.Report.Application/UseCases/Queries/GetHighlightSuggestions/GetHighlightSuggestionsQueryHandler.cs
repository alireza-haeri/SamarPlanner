using MediatR;
using SamarPlanner.Report.Application.Abstractions;
using SamarPlanner.Shared.Contracts.Queries;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Report.Application.UseCases.Queries.GetHighlightSuggestions;

public class GetHighlightSuggestionsQueryHandler(IReportRepository reportRepository)
    : IRequestHandler<GetHighlightSuggestionsQuery, Result<GetHighlightSuggestionsQueryResponse>>
{
    public async Task<Result<GetHighlightSuggestionsQueryResponse>> Handle(GetHighlightSuggestionsQuery request,
        CancellationToken cancellationToken)
    {
        var suggestions =
            await reportRepository.GetHighlightSuggestionsAsync(request.UserId, request.Text, cancellationToken);
        
        return Result<GetHighlightSuggestionsQueryResponse>.Success(
            new GetHighlightSuggestionsQueryResponse(
                suggestions
                    .Select(s =>
                        new GetHighlightSuggestionsQueryResponseSuggestion(s.Text, s.Type))
                    .ToList()
            ));
    }
}