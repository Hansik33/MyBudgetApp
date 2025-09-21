using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class SavingService(IDialogService dialogService, IDatabaseService databaseService) : ISavingService
    {
        private static Saving CreateSaving(int userId, int goalId, decimal amount, DateTime date)
        {
            return new Saving
            {
                UserId = userId,
                GoalId = goalId,
                Amount = amount,
                Date = date
            };
        }

        public async Task<List<Saving>> GetSavingsAsync(int userId) =>
            await databaseService.GetSavingsAsync(userId);

        public async Task<Saving?> AddSavingAsync(int userId,
                                                  IEnumerable<SavingGoalViewModel> savingGoals,
                                                  decimal currentBalance)
        {
            var viewModel = await dialogService.ShowAddSavingDialogAsync(savingGoals, currentBalance);

            if (viewModel != null)
            {
                var saving = CreateSaving(userId, viewModel.SelectedSavingGoalId, viewModel.AmountAsDecimal, DateTime.Today);
                return await databaseService.AddSavingAsync(saving);
            }
            return null;
        }

        public async Task<bool> DeleteSavingAsync(int savingId)
        {
            if (!await dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Saving.ConfirmDelete))
                return false;

            if (await databaseService.DeleteSavingAsync(savingId))
                return true;

            return false;
        }
    }
}