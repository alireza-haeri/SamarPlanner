using Microsoft.JSInterop;

namespace SamarPlanner.Maui;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider) 
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage());
    }
}
