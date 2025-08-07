using MyBudgetApp.Enums;
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
    public class SavingGoalService(IDialogService dialogService, IDatabaseService databaseService) : ISavingGoalService
    {
        private static SavingGoal CreateSavingGoal(int userId, string name, decimal targetAmount, DateTime deadline)
        {
            return new SavingGoal
            {
                UserId = userId,
                Name = name,
                TargetAmount = targetAmount,
                Deadline = deadline
            };
        }

        public async Task<List<SavingGoal>> GetSavingGoalsAsync(int userId) =>
            await databaseService.GetSavingGoalsAsync(userId);

        public async Task<SavingGoal?> AddSavingGoalAsync(int userId, IEnumerable<SavingGoalViewModel> savingGoals)
        {
            var viewModel = await dialogService.ShowAddSavingGoalDialogAsync(savingGoals);

            if (viewModel != null)
            {
                var savingGoal = CreateSavingGoal(userId,
                                                  viewModel.Name,
                                                  viewModel.TargetAmountAsDecimal,
                                                  viewModel.SelectedDeadlineAsDateTime);
                return await databaseService.AddSavingGoalAsync(savingGoal);
            }
            return null;
        }

        public async Task<bool> DeleteSavingGoalAsync(int savingGoalId)
        {
            var confirmed = await dialogService.ShowConfirmationAsync(AppStrings.Dialogs.SavingGoal.ConfirmDelete);

            if (!confirmed)
                return false;

            await databaseService.DeleteSavingGoalAsync(savingGoalId);
            await dialogService.ShowMessageAsync(AppStrings.Dialogs.SavingGoal.DeletedSuccess, DialogType.Success);

            return true;
        }
    }
}