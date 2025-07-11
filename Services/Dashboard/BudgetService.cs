using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class BudgetService(IDatabaseService databaseService) : IBudgetService
    {
        public async Task DeleteBudgetAsync(int budgetId) => await databaseService.DeleteBudgetAsync(budgetId);
    }
}
