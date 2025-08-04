using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class SavingGoalService(IDatabaseService databaseService) : ISavingGoalService
    {
        public async Task<List<SavingGoal>> GetSavingGoalsAsync(int userId) =>
            await databaseService.GetSavingGoalsAsync(userId);
        public async Task<SavingGoal> AddSavingGoalAsync(SavingGoal savingGoal) =>
            await databaseService.AddSavingGoalAsync(savingGoal);
        public async Task DeleteSavingGoalAsync(int savingGoalId) =>
            await databaseService.DeleteSavingGoalAsync(savingGoalId);
    }
}