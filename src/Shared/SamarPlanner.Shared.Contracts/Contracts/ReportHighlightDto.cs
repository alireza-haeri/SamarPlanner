using SamarPlanner.Shared.Contracts.Enums;

namespace SamarPlanner.Shared.Contracts.Contracts;

public record ReportHighlightDto(string Text, ReportHighlightType Type);