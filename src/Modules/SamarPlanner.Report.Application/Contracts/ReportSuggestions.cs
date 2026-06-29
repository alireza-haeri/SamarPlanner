using SamarPlanner.Report.Core.Enums;

namespace SamarPlanner.Report.Application.Contracts;

public record ReportSuggestions(string Text, ReportHighlightType Type);