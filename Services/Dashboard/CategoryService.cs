using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
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

        public async Task DeleteCategoryAsync(int categoryId) => await databaseService.DeleteCategoryAsync(categoryId);
    }
}