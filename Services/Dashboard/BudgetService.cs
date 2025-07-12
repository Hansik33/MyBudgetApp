using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class BudgetService(IDatabaseService databaseService) : IBudgetService
    {
        public async Task<List<Budget>> GetBudgetsAsync(int userId) => await databaseService.GetBudgetsAsync(userId);
        public async Task DeleteBudgetAsync(int budgetId) => await databaseService.DeleteBudgetAsync(budgetId);
    }
}