using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingGoalService
    {
        Task<List<SavingGoal>> GetSavingGoalsAsync(int userId);
        Task DeleteSavingGoalAsync(int savingGoalId);
    }
}
