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
        Task<bool> DeleteCategoryAsync(CategoryViewModel category,
                                       IEnumerable<BudgetViewModel> budgets,
                                       IEnumerable<TransactionViewModel> transactions);
    }
}