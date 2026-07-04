namespace SamarPlanner.Web.Shared.Abstractions;

public interface IToastService
{
    void ShowToastInfo(string message, string title = "نکته");
    void ShowToastSuccess(string message, string title = "موفقیت");
    void ShowToastError(string message, string title = "خطا");
    void ShowToastWarning(string message, string title = "هشدار");
}