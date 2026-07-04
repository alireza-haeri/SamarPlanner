using Android.App;
using SamarPlanner.Web.Shared.Services;
using WebView = Android.Webkit.WebView;

namespace SamarPlanner.Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        var backNavService = IPlatformApplication.Current?.Services.GetService<BackNavigationService>();

        if (backNavService?.CanGoBack == true)
        {
            backNavService.GoBack();
            return true;
        }

        return base.OnBackButtonPressed();
    }
}