using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DialogService
    {
        private XamlRoot? _xamlRoot;

        public void SetXamlRoot(XamlRoot root) => _xamlRoot = root;

        public async Task ShowMessageAsync(string message)
        {
            if (_xamlRoot is null)
                throw new InvalidOperationException("XamlRoot is not set.");

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
