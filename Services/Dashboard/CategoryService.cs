using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators.Dashboard;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class CategoryService(IDialogService dialogService, IDatabaseService databaseService) : ICategoryService
    {
        private static Category CreateCategory(int userId, string name)
        {
            return new Category
            {
                UserId = userId,
                Name = name
            };
        }

        public async Task<List<Category>> GetCategoriesAsync(int userId) => await databaseService.GetCategoriesAsync(userId);

        public async Task<Category?> AddCategoryAsync(int userId, IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = await dialogService.ShowAddCategoryDialogAsync(categories);

            if (viewModel != null)
            {
                var category = CreateCategory(userId, viewModel.Name);
                return await databaseService.AddCategoryAsync(category);
            }
            return null;
        }

        public async Task<bool> DeleteCategoryAsync(CategoryViewModel category,
                                                    IEnumerable<BudgetViewModel> budgets,
                                                    IEnumerable<TransactionViewModel> transactions)
        {
            var confirmed = await dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Category.ConfirmDelete);

            if (!confirmed)
                return false;

            if (CategoryValidator.IsDeletionAllowed(category, budgets, transactions))
            {
                await databaseService.DeleteCategoryAsync(category.Id);
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.Category.DeletedSuccess, DialogType.Success);

                return true;
            }
            else
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.Category.DeletionNotAllowed, DialogType.Error);
                return false;
            }
        }
    }
}