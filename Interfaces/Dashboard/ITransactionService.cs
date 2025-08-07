using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync(int userId);
        Task<Transaction?> AddTransactionAsync(int userId, IEnumerable<CategoryViewModel> categories);
        Task<bool> DeleteTransactionAsync(TransactionViewModel transaction, decimal currentBalance);
    }
}
