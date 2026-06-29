using MediatR;
using SamarPlanner.Report.Core.Enums;
using SamarPlanner.Shared.Kernel;

namespace SamarPlanner.Shared.Contracts.Queries;

public record GetHighlightSuggestionsQuery(Guid UserId, string Text) : IRequest<Result<GetHighlightSuggestionsQueryResponse>>;

public record GetHighlightSuggestionsQueryResponse(List<GetHighlightSuggestionsQueryResponseSuggestion> Suggestions);
public record GetHighlightSuggestionsQueryResponseSuggestion(string Text, ReportHighlightType Type);