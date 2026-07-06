namespace SamarPlanner.Note.Core.Contracts;

public static class AllowedFileTypes
{
   public static readonly IReadOnlyDictionary<string, string> ContentTypeToExtension = 
    new Dictionary<string, string>
    {
        ["image/png"] = "png",
        ["image/jpeg"] = "jpg",
        ["image/gif"] = "gif",
        ["image/webp"] = "webp",
        ["image/svg+xml"] = "svg",
        ["image/bmp"] = "bmp",
        ["image/x-icon"] = "ico",
        ["image/tiff"] = "tiff",
        ["image/heic"] = "heic",  
        ["image/avif"] = "avif",  

        ["video/mp4"] = "mp4",
        ["video/webm"] = "webm",
        ["video/ogg"] = "ogv",
        ["video/quicktime"] = "mov",
        ["video/x-matroska"] = "mkv",
        ["video/mpeg"] = "mpeg",
        ["video/avi"] = "avi",

        ["audio/mpeg"] = "mp3",
        ["audio/wav"] = "wav",
        ["audio/ogg"] = "ogg",
        ["audio/aac"] = "aac",
        ["audio/flac"] = "flac",
        ["audio/mp4"] = "m4a", 
        ["application/pdf"] = "pdf",
        ["application/msword"] = "doc",
        ["application/vnd.openxmlformats-officedocument.wordprocessingml.document"] = "docx",
        ["application/vnd.ms-excel"] = "xls",
        ["application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"] = "xlsx",
        ["application/vnd.ms-powerpoint"] = "ppt",
        ["application/vnd.openxmlformats-officedocument.presentationml.presentation"] = "pptx",
        ["application/rtf"] = "rtf",
        ["application/vnd.oasis.opendocument.text"] = "odt",
        ["application/vnd.oasis.opendocument.spreadsheet"] = "ods",
        ["application/vnd.oasis.opendocument.presentation"] = "odp",

        ["text/plain"] = "txt",
        ["text/markdown"] = "md",
        ["text/csv"] = "csv",
        ["application/json"] = "json",
        ["application/xml"] = "xml",
        ["text/xml"] = "xml",
        ["text/yaml"] = "yaml",
        ["application/x-yaml"] = "yaml",
        ["text/css"] = "css",
        ["text/scss"] = "scss",
        ["text/javascript"] = "js",
        ["application/typescript"] = "ts",
        
        ["text/x-csharp"] = "cs",
        ["text/x-python"] = "py",
        ["text/x-java"] = "java",
        ["text/x-c"] = "c",
        ["text/x-c++"] = "cpp",
        ["text/x-go"] = "go",
        ["text/x-rust"] = "rs",
        ["text/x-php"] = "php",

        ["font/ttf"] = "ttf",
        ["font/otf"] = "otf",
        ["font/woff"] = "woff",
        ["font/woff2"] = "woff2",

        ["application/zip"] = "zip",
        ["application/x-rar-compressed"] = "rar",
        ["application/x-7z-compressed"] = "7z",
        ["application/gzip"] = "gz",
        ["application/x-tar"] = "tar"
    };
}