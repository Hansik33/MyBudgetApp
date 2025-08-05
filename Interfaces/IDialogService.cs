using Microsoft.UI.Xaml;
using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces
{
    public interface IDialogService
    {
        void SetXamlRoot(XamlRoot root);

        Task ShowMessageAsync(string message, DialogType dialogType = DialogType.Info);
        Task<bool> ShowConfirmationAsync(string message);

        Task<Budget?> ShowAddBudgetDialogAsync(IEnumerable<BudgetViewModel> budgets, IEnumerable<CategoryViewModel> categories);
        Task<Category?> ShowAddCategoryDialogAsync(IEnumerable<CategoryViewModel> categories);
        Task<Transaction?> ShowAddTransactionDialogAsync(IEnumerable<CategoryViewModel> categories);
        Task<SavingGoal?> ShowAddSavingGoalDialogAsync(IEnumerable<SavingGoalViewModel> savingGoals);
    }
}