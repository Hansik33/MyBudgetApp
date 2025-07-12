using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ISavingService
    {
        Task<List<Saving>> GetSavingsAsync(int userId);
        Task DeleteSavingAsync(int savingId);
    }
}
