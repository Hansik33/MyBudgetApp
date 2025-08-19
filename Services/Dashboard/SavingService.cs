using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class SavingService(IDialogService dialogService, IDatabaseService databaseService) : ISavingService
    {
        public async Task<List<Saving>> GetSavingsAsync(int userId) =>
            await databaseService.GetSavingsAsync(userId);

        public async Task<Saving?> AddSavingAsync(int userId, IEnumerable<SavingGoalViewModel> savingGoals)
        {
            _ = await dialogService.ShowAddSavingDialogAsync(savingGoals);

            return null;
        }

        public async Task DeleteSavingAsync(int savingId) =>
            await databaseService.DeleteSavingAsync(savingId);
    }
}