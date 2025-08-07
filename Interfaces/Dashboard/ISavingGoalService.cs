using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingGoalService
    {
        Task<List<SavingGoal>> GetSavingGoalsAsync(int userId);
        Task<SavingGoal?> AddSavingGoalAsync(int userId, IEnumerable<SavingGoalViewModel> savingGoals);
        Task DeleteSavingGoalAsync(int savingGoalId);
    }
}
