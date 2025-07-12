using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class SavingService(IDatabaseService databaseService) : ISavingService
    {
        public async Task<List<Saving>> GetSavingsAsync(int userId) =>
            await databaseService.GetSavingsAsync(userId);
        public async Task DeleteSavingAsync(int savingId) =>
            await databaseService.DeleteSavingAsync(savingId);
    }
}