using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public static class DialogService
    {
        public static async Task ShowMessageAsync(string message, XamlRoot root)
        {
            var dialog = new ContentDialog
            {
                Title = "Informacja",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = root
            };
            await dialog.ShowAsync();
        }
    }
}
