namespace SamarPlanner.Shared.Kernel;

public sealed class ApplicationSettings
{
    public required JwtTokenSettings JwtToken { get; set; }
    public required DatabaseSettings Databases { get; set; }
    public required CorsPolicySettings CorsPolicy { get; set; }
}

public sealed class DatabaseSettings
{
    public required string IdentityConnectionString { get; set; }
    public required string GoalConnectionString { get; set; }
    public required string  TaskConnectionString { get; set; }
    public required string  ReportConnectionString { get; set; }
}

public sealed class JwtTokenSettings
{
    public required string SigningKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpiresInMinutes { get; set; } = 10080;
}

public sealed class CorsPolicySettings
{
    public required string[] Origins { get; set; }
}