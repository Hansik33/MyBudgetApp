using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DialogService
    {
        private XamlRoot? _xamlRoot;

        public void SetXamlRoot(XamlRoot xamlRoot) => _xamlRoot = xamlRoot;

        public async Task ShowMessageAsync(string message)
        {
            if (_xamlRoot is null)
                throw new InvalidOperationException("XamlRoot is not set. Call SetXamlRoot before showing dialogs.");

            var dialog = new ContentDialog
            {
                Title = "Informacja",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = _xamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}