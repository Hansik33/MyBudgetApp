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

        Task<string?> ShowAddCategoryDialogAsync();
        Task<Budget?> ShowAddBudgetDialogAsync(IEnumerable<CategoryViewModel> categories);
    }
}