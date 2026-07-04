using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace SamarPlanner.Web.Shared.Services;

public class BackNavigationService
{
    private readonly Stack<string> _history = new();
    private NavigationManager? _navigationManager;
    private string? _lastStableUri;
    private CancellationTokenSource? _debounceCts;

    public bool CanGoBack => _history.Count > 0;

    public void Initialize(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
        _lastStableUri = navigationManager.Uri;
        navigationManager.LocationChanged += OnLocationChanged;
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        // یه ناوبری جدید اومد؛ هر چیزی که قبلاً منتظرش بودیم رو کنسل کن
        _debounceCts?.Cancel();
        var cts = new CancellationTokenSource();
        _debounceCts = cts;

        var fromUri = _lastStableUri;
        var toUri = e.Location;

        _ = CommitAfterSettling(fromUri, toUri, cts.Token);
    }

    private async Task CommitAfterSettling(string? fromUri, string toUri, CancellationToken token)
    {
        try
        {
            await Task.Delay(150, token);
        }
        catch (TaskCanceledException)
        {
            return; // یه ناوبری دیگه فوراً بعدش اومد؛ این یکی رو نادیده بگیر (مثل ریدایرکت auth guard)
        }

        if (fromUri != null && fromUri != toUri)
            _history.Push(fromUri);

        _lastStableUri = toUri;
    }

    public bool GoBack()
    {
        if (_navigationManager == null || _history.Count == 0)
            return false;

        var previous = _history.Pop();
        _lastStableUri = previous;
        _debounceCts?.Cancel(); // مطمئن شو خودِ برگشت باعث push جدید نمی‌شه
        _navigationManager.NavigateTo(previous);
        return true;
    }
}