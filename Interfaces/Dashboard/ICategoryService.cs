using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync(int userId);
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}