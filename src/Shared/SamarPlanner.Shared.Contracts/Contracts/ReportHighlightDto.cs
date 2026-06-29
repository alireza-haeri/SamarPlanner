using SamarPlanner.Report.Core.Enums;

namespace SamarPlanner.Shared.Contracts.Contracts;

public record ReportHighlightDto(string Text, ReportHighlightType Type);