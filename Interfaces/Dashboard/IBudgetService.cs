using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface IBudgetService
    {
        Task<List<Budget>> GetBudgetsAsync(int userId);
        Task AddBudgetAsync(Budget budget);
        Task DeleteBudgetAsync(int budgetId);
    }
}
