using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Activity;
using AndroidX.Core.View;
using Microsoft.Maui.LifecycleEvents;
using SamarPlanner.Web.Shared.Services;
using View = Android.Views.View;

namespace SamarPlanner.Maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        ViewCompat.SetOnApplyWindowInsetsListener(Window!.DecorView, new InsetsListener());

        OnBackPressedDispatcher.AddCallback(this, new BackPressedCallback(this));
    }

    private class InsetsListener : Java.Lang.Object, IOnApplyWindowInsetsListener
    {
        public WindowInsetsCompat? OnApplyWindowInsets(View? view, WindowInsetsCompat? insets)
        {
            var systemBars = insets?.GetInsets(WindowInsetsCompat.Type.SystemBars());
            if (systemBars != null)
                view?.SetPadding(systemBars.Left, systemBars.Top, systemBars.Right, systemBars.Bottom);
            return insets;
        }
    }

    private class BackPressedCallback(MainActivity activity) : OnBackPressedCallback(true)
    {
        public override void HandleOnBackPressed()
        {
            var backNavService = IPlatformApplication.Current?.Services.GetService<BackNavigationService>();
            Android.Util.Log.Debug("BackNav", $"HandleOnBackPressed fired. Service null? {backNavService == null}, CanGoBack: {backNavService?.CanGoBack}");

            if (backNavService?.CanGoBack == true)
            {
                backNavService.GoBack();
                return;
            }

            Enabled = false;
            activity.OnBackPressedDispatcher.OnBackPressed();
            Enabled = true;
        }
    }
}