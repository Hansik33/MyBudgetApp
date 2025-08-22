using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingService
    {
        Task<List<Saving>> GetSavingsAsync(int userId);
        Task<Saving?> AddSavingAsync(int userId, IEnumerable<SavingGoalViewModel> savingGoals, decimal currentBalance);
        Task<bool> DeleteSavingAsync(int savingId);
    }
}
