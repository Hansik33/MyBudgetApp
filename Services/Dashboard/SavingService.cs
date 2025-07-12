using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class SavingService(IDatabaseService databaseService) : ISavingService
    {
        public async Task DeleteSavingAsync(int savingId) =>
            await databaseService.DeleteSavingAsync(savingId);
    }
}