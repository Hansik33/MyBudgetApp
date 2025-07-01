using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using System;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DialogService : IDialogService
    {
        private XamlRoot? _xamlRoot;

        public void SetXamlRoot(XamlRoot root) => _xamlRoot = root;

        public async Task ShowMessageAsync(string message, DialogType dialogType)
        {
            if (_xamlRoot is null)
                return;

            var title = dialogType switch
            {
                DialogType.Warning => AppStrings.Dialogs.TitleWarning,
                DialogType.Error => AppStrings.Dialogs.TitleError,
                DialogType.Success => AppStrings.Dialogs.TitleSuccess,
                _ => AppStrings.Dialogs.TitleInfo
            };

            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = _xamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}