namespace SamarPlanner.Web.Services;

public interface IToastService
{
    void ShowToastInfo(string message, string title = "نکته");
    void ShowToastSuccess(string message, string title = "موفقیت");
    void ShowToastError(string message, string title = "خطا");
    void ShowToastWarning(string message, string title = "هشدار");
}

public class ToastService(Blazored.Toast.Services.IToastService toastService) : IToastService
{
    public void ShowToastInfo(string message, string title = "نکته")
        => toastService.ShowInfo(message, title);

    public void ShowToastSuccess(string message, string title = "موفقیت")
        => toastService.ShowSuccess(message, title);

    public void ShowToastError(string message, string title = "خطا")
        => toastService.ShowError(message, title);

    public void ShowToastWarning(string message, string title = "هشدار")
        => toastService.ShowWarning(message, title);
}