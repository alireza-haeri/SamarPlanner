using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SamarPlanner.Shared.Swagger;

public class SummaryFromOperationIdFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (string.IsNullOrWhiteSpace(operation.Summary) && !string.IsNullOrWhiteSpace(operation.OperationId))

            operation.Summary = operation.OperationId;
    }
}