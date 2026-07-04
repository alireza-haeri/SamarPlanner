using Microsoft.AspNetCore.Components;

namespace SamarPlanner.Web.Shared.Services;

public class ModalService
{
    public event Action? OnChanged;

    public RenderFragment? Content { get; private set; }
    public bool IsOpen => Content is not null;

    public void Show(RenderFragment content)
    {
        Content = content;
        OnChanged?.Invoke();
    }

    public void Close()
    {
        Content = null;
        OnChanged?.Invoke();
    }
}