using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace SamarPlanner.Web.Landing.Pages;

public class IndexModel(IOptions<ApplicationSettings> options) : PageModel
{
    private readonly ApplicationSettings _settings = options.Value;

    public string? WebUrl { get; set; }

    public void OnGet()
    {
        WebUrl = _settings.WebUrl;
    }
}