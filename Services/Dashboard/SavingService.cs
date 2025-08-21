using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
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

        public async Task<bool> DeleteSavingAsync(int savingId)
        {
            var confirmed = await dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Saving.ConfirmDelete);

            if (!confirmed)
                return false;

            await databaseService.DeleteSavingAsync(savingId);
            await dialogService.ShowMessageAsync(AppStrings.Dialogs.Saving.DeletedSuccess, DialogType.Success);

            return true;
        }
    }
}