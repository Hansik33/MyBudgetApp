using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingService
    {
        Task DeleteSavingAsync(int savingId);
    }
}
