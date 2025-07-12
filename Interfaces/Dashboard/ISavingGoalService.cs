using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingGoalService
    {
        Task DeleteSavingGoalAsync(int savingGoalId);
    }
}
