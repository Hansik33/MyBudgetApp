using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class TransactionService(IDatabaseService databaseService) : ITransactionService
    {
        public async Task DeleteTransactionAsync(int transactionId) =>
            await databaseService.DeleteTransactionAsync(transactionId);
    }
}