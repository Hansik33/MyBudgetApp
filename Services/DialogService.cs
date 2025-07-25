using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MyBudgetApp.ViewModels.Dashboard;
using MyBudgetApp.ViewModels.Dashboard.Dialogs;
using MyBudgetApp.Views.Dashboard.Dialogs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DialogService : IDialogService
    {
        private XamlRoot? _xamlRoot;
        private bool _isDialogOpen = false;

        public void SetXamlRoot(XamlRoot root) => _xamlRoot = root;

        public async Task ShowMessageAsync(string message, DialogType dialogType)
        {
            if (_xamlRoot is null || _isDialogOpen)
                return;

            _isDialogOpen = true;

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
            _isDialogOpen = false;
        }

        public async Task<bool> ShowConfirmationAsync(string message)
        {
            if (_xamlRoot is null || _isDialogOpen)
                return false;

            _isDialogOpen = true;

            var dialog = new ContentDialog
            {
                Title = AppStrings.Dialogs.TitleWarning,
                Content = message,
                PrimaryButtonText = "Tak",
                CloseButtonText = "Nie",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = _xamlRoot
            };

            var result = await dialog.ShowAsync();
            _isDialogOpen = false;

            return result == ContentDialogResult.Primary;
        }

        public async Task<string?> ShowAddCategoryDialogAsync()
        {
            var viewModel = new AddCategoryDialogViewModel();
            var dialog = new AddCategoryDialog
            {
                DataContext = viewModel,
                XamlRoot = _xamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
                return viewModel.CategoryName;

            return null;
        }


        public async Task<Budget?> ShowAddBudgetDialogAsync(IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = new AddBudgetDialogViewModel(categories);
            var dialog = new AddBudgetDialog
            {
                DataContext = viewModel,
                XamlRoot = _xamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                return new Budget
                {
                    CategoryId = viewModel.SelectedCategory?.Id ?? 0,
                    MonthNumber = viewModel.SelectedMonthNumber,
                    Year = viewModel.SelectedYearNumber,
                    LimitAmount = viewModel.LimitAmountDecimal
                };
            }
            return null;
        }
    }
}