using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class BudgetService(IDialogService dialogService, IDatabaseService databaseService) : IBudgetService
    {
        private static Budget CreateBudget(int userId, int categoryId, int monthNumber, int year, decimal limitAmount)
        {
            return new Budget
            {
                UserId = userId,
                CategoryId = categoryId,
                MonthNumber = monthNumber,
                Year = year,
                LimitAmount = limitAmount
            };
        }

        public async Task<List<Budget>> GetBudgetsAsync(int userId) => await databaseService.GetBudgetsAsync(userId);

        public async Task<Budget?> AddBudgetAsync(int userId,
                                                 IEnumerable<BudgetViewModel> budgets,
                                                 IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = await dialogService.ShowAddBudgetDialogAsync(budgets, categories);

            if (viewModel != null)
            {
                var budget = CreateBudget(userId,
                                          viewModel.SelectedCategoryId,
                                          viewModel.SelectedMonthNumber,
                                          viewModel.SelectedYearNumber,
                                          viewModel.LimitAmountDecimal);
                return await databaseService.AddBudgetAsync(budget);
            }
            return null;
        }

        public async Task<bool> DeleteBudgetAsync(int budgetId)
        {
            var confirmed = await dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Budget.ConfirmDelete);

            if (!confirmed)
                return false;

            await databaseService.DeleteBudgetAsync(budgetId);
            await dialogService.ShowMessageAsync(AppStrings.Dialogs.Budget.DeletedSuccess, DialogType.Success);

            return true;
        }
    }
}