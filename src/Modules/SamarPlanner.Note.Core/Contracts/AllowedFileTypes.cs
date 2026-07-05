namespace SamarPlanner.Note.Core.Contracts;

public static class AllowedFileTypes
{
    public static readonly IReadOnlyDictionary<string, string> ContentTypeToExtension = 
        new Dictionary<string, string>
        {
            ["image/png"] = "png",
            ["image/jpeg"] = "jpg",
            ["image/webp"] = "webp",
            ["application/pdf"] = "pdf",
        };
}