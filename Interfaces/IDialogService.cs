using Microsoft.UI.Xaml;
using MyBudgetApp.Enums;
using MyBudgetApp.Enums.ValidationResults;
using MyBudgetApp.ViewModels.Dashboard;
using MyBudgetApp.ViewModels.Dashboard.Dialogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces
{
    public interface IDialogService
    {
        void SetXamlRoot(XamlRoot root);

        Task ShowMessageAsync(string message, DialogType dialogType = DialogType.Info);
        Task<bool> ShowConfirmationAsync(string message);

        Task<AuthenticationValidationResult> ShowAuthenticationValidationDialogAsync(string username, string password);
        Task<AuthenticationValidationResult> ShowAuthenticationValidationDialogAsync(string username,
                                                                                     string password,
                                                                                     string confirmPassword);

        Task<AddBudgetDialogViewModel?> ShowAddBudgetDialogAsync(IEnumerable<BudgetViewModel> budgets,
                                                                 IEnumerable<CategoryViewModel> categories);
        Task<AddCategoryDialogViewModel?> ShowAddCategoryDialogAsync(IEnumerable<CategoryViewModel> categories);
        Task<AddTransactionDialogViewModel?> ShowAddTransactionDialogAsync(IEnumerable<CategoryViewModel> categories,
                                                                           decimal currentBalance);
        Task<AddSavingDialogViewModel?> ShowAddSavingDialogAsync(IEnumerable<SavingGoalViewModel> savingGoals,
                                                                 decimal currentBalance);
        Task<AddSavingGoalDialogViewModel?> ShowAddSavingGoalDialogAsync(IEnumerable<SavingGoalViewModel> savingGoals);
    }
}