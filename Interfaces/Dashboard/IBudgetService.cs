using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface IBudgetService
    {
        Task<List<Budget>> GetBudgetsAsync(int userId);
        Task<Budget?> AddBudgetAsync(int userId, IEnumerable<BudgetViewModel> budgets, IEnumerable<CategoryViewModel> categories);
        Task DeleteBudgetAsync(int budgetId);
    }
}