using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

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

    public static Result<TModel> UnAuthorizeFailure()=>
        new()
        {
            IsSuccess = false,
            Errors = new Dictionary<string, string[]>
                { { "General", ["شما دسترسی لازم را ندارید. لطفاً وارد شوید."] } },
            BadResultType =  Kernel.BadResultType.Unauthorized
        };
}

[JsonConverter(typeof(JsonStringEnumConverter))]
[DataContract(Name = "BadResultType")]
[SwaggerSchema(Title = "BadResultType")]
public enum BadResultType
{
    [EnumMember(Value = "NotFound")]
    NotFound = 0,
    [EnumMember(Value = "Validation")]
    Validation = 1,
    [EnumMember(Value = "General")]
    General = 2,
    [EnumMember(Value = "Unauthorized")]
    Unauthorized = 3,
}