using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces.Dashboard
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync(int userId);
        Task DeleteTransactionAsync(int transactionId);
    }
}
