using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators;
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

        private async Task<BudgetValidationResult> ShowBudgetValidationDialog(string limitAmount,
                                                                                   IEnumerable<CategoryViewModel> categories)
        {
            var result = BudgetValidator.Validate(limitAmount, categories);

            switch (result)
            {
                case BudgetValidationResult.Empty:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.LimitEmpty, DialogType.Error);
                    break;
                case BudgetValidationResult.NotANumber:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.LimitNotANumber, DialogType.Error);
                    break;
                case BudgetValidationResult.Negative:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.LimitNegative, DialogType.Error);
                    break;
                case BudgetValidationResult.Zero:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.LimitZero, DialogType.Error);
                    break;
                case BudgetValidationResult.TooLarge:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.LimitTooTooLarge, DialogType.Error);
                    break;
                case BudgetValidationResult.CategoryNotSelected:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.CategoryNotSelected, DialogType.Error);
                    break;
                case BudgetValidationResult.Success:
                    await ShowMessageAsync(AppStrings.Dialogs.Budget.CreatedSuccess, DialogType.Success);
                    break;
            }
            return result;
        }

        public async Task<Budget?> ShowAddBudgetDialogAsync(IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = new AddBudgetDialogViewModel(categories);

            while (true)
            {
                var dialog = new AddBudgetDialog
                {
                    DataContext = viewModel,
                    XamlRoot = _xamlRoot
                };

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var validationResult = await ShowBudgetValidationDialog(viewModel.LimitAmount, categories);
                    if (validationResult == BudgetValidationResult.Success)
                    {
                        return new Budget
                        {
                            CategoryId = viewModel.SelectedCategory?.Id ?? 0,
                            MonthNumber = viewModel.SelectedMonthNumber,
                            Year = viewModel.SelectedYearNumber,
                            LimitAmount = decimal.TryParse(viewModel.LimitAmount, out var limit) ? limit : 0
                        };
                    }
                    continue;
                }
                return null;
            }
        }

        private async Task<CategoryNameValidationResult>
            ShowCategoryNameValidationDialog(string categoryName, IEnumerable<CategoryViewModel> categories)
        {
            var result = CategoryValidator.ValidateName(categoryName, categories);

            switch (result)
            {
                case CategoryNameValidationResult.Success:
                    await ShowMessageAsync(AppStrings.Dialogs.Category.CreatedSuccess, DialogType.Success);
                    break;
                case CategoryNameValidationResult.Empty:
                    await ShowMessageAsync(AppStrings.Dialogs.Category.NameEmpty, DialogType.Error);
                    break;
                case CategoryNameValidationResult.TooShort:
                    await ShowMessageAsync(AppStrings.Dialogs.Category.NameTooShort, DialogType.Error);
                    break;
                case CategoryNameValidationResult.TooLong:
                    await ShowMessageAsync(AppStrings.Dialogs.Category.NameTooLong, DialogType.Error);
                    break;
                case CategoryNameValidationResult.NotUnique:
                    await ShowMessageAsync(AppStrings.Dialogs.Category.NameExists, DialogType.Error);
                    break;
            }
            return result;
        }

        public async Task<Category?> ShowAddCategoryDialogAsync(IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = new AddCategoryDialogViewModel();

            while (true)
            {
                var dialog = new AddCategoryDialog
                {
                    DataContext = viewModel,
                    XamlRoot = _xamlRoot
                };

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var validationResult = await ShowCategoryNameValidationDialog(viewModel.CategoryName, categories);
                    if (validationResult == CategoryNameValidationResult.Success)
                        return new Category { Name = viewModel.CategoryName };
                    continue;
                }
                return null;
            }
        }

        private async Task<TransactionValidationResult>
            ShowTransactionValidationDialog(string transactionAmount,
                                            string description,
                                            DateTime transactionDate,
                                            IEnumerable<CategoryViewModel> categories)
        {
            var result = TransactionValidator.Validate(transactionAmount, description, transactionDate, categories);
            switch (result)
            {
                case TransactionValidationResult.AmountEmpty:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.AmountEmpty, DialogType.Error);
                    break;
                case TransactionValidationResult.AmountNotANumber:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.AmountNotANumber, DialogType.Error);
                    break;
                case TransactionValidationResult.AmountNegative:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.AmountNegative, DialogType.Error);
                    break;
                case TransactionValidationResult.AmountZero:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.AmountZero, DialogType.Error);
                    break;
                case TransactionValidationResult.AmountTooLarge:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.AmountTooLarge, DialogType.Error);
                    break;
                case TransactionValidationResult.DescriptionTooShort:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.DescriptionTooShort, DialogType.Error);
                    break;
                case TransactionValidationResult.DescriptionTooLong:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.DescriptionTooLong, DialogType.Error);
                    break;
                case TransactionValidationResult.DateInvalid:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.DateInvalid, DialogType.Error);
                    break;
                case TransactionValidationResult.CategoryNotSelected:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.CategoryNotSelected, DialogType.Error);
                    break;
                case TransactionValidationResult.Success:
                    await ShowMessageAsync(AppStrings.Dialogs.Transaction.CreatedSuccess, DialogType.Success);
                    break;
            }
            return result;
        }

        public async Task<Transaction?> ShowAddTransactionDialogAsync(IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = new AddTransactionDialogViewModel(categories);

            while (true)
            {
                var dialog = new AddTransactionDialog
                {
                    DataContext = viewModel,
                    XamlRoot = _xamlRoot
                };

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var validationResult = await ShowTransactionValidationDialog(
                        viewModel.Amount,
                        viewModel.Description,
                        viewModel.SelectedDateAsDateTime,
                        categories);
                    if (validationResult == TransactionValidationResult.Success)
                    {
                        return new Transaction
                        {
                            Type = viewModel.SelectedType?.Value ?? TransactionType.Expense,
                            CategoryId = viewModel.SelectedCategory?.Id ?? 0,
                            Amount = decimal.TryParse(viewModel.Amount, out var amount) ? amount : 0,
                            Method = viewModel.SelectedMethod?.Value ?? PaymentMethod.Cash,
                            Description = viewModel.Description == string.Empty ? "Brak" : viewModel.Description,
                            Date = viewModel.SelectedDateAsDateTime
                        };
                    }
                    continue;
                }
                return null;
            }
        }
    }
}