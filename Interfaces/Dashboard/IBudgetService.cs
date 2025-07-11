using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface IBudgetService
    {
        Task DeleteBudgetAsync(int budgetId);
    }

}
