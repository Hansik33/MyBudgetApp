using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync(int userId);
        Task<Category?> AddCategoryAsync(int userId, IEnumerable<CategoryViewModel> categories);
        Task DeleteCategoryAsync(int categoryId);
    }
}