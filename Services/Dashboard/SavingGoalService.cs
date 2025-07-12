using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class SavingGoalService(IDatabaseService databaseService) : ISavingGoalService
    {
        public async Task DeleteSavingGoalAsync(int savingGoalId) =>
            await databaseService.DeleteSavingGoalAsync(savingGoalId);
    }
}