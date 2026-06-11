using System.Text.Json.Serialization;

namespace SamarPlanner.Shared.Kernel;

public class Result<TModel>
{
    public bool IsSuccess { get; private set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BadResultType? BadResultType { get; private set; }
    public TModel? Response { get; private set; }
    public IDictionary<string, string[]>? Errors { get; private set; }

    public static Result<TModel> Success(TModel response) =>
        new() { IsSuccess = true, Response = response };

    public static Result<TModel> GeneralFailure(string generalError = "خطایی رخ داده است!") =>
        new()
        {
            IsSuccess = false,
            Errors = new Dictionary<string, string[]>
                { { "General", [generalError] } },
            BadResultType =  Kernel.BadResultType.General
        };

    public static Result<TModel> ValidationFailure(IDictionary<string, string[]> validationErrors) =>
        new() { IsSuccess = false, Errors = validationErrors, BadResultType = Kernel.BadResultType.Validation };

    public static Result<TModel> NotfoundFailure(string message, string? key = null) =>
        new()
        {
            IsSuccess = false,
            Errors = new Dictionary<string, string[]> { { key ?? "General", [message] } },
            BadResultType =  Kernel.BadResultType.NotFound
        };
}

public enum BadResultType
{
    NotFound = 0,
    Validation = 1,
    General = 2
}