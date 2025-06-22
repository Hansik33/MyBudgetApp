using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

public static class DialogService
{
    private static XamlRoot? _xamlRoot;

    public static void SetXamlRoot(XamlRoot xamlRoot) => _xamlRoot = xamlRoot;

    public static async Task ShowMessageAsync(string message)
    {
        var dialog = new ContentDialog
        {
            Title = "Informacja",
            Content = message,
            CloseButtonText = "OK",
            XamlRoot = _xamlRoot!
        };

        await dialog.ShowAsync();
    }
}
