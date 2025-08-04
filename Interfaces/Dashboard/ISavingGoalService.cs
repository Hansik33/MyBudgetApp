using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingGoalService
    {
        Task<List<SavingGoal>> GetSavingGoalsAsync(int userId);
        Task<SavingGoal> AddSavingGoalAsync(SavingGoal savingGoal);
        Task DeleteSavingGoalAsync(int savingGoalId);
    }
}
