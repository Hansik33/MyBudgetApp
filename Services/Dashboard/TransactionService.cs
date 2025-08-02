using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class TransactionService(IDatabaseService databaseService) : ITransactionService
    {
        public async Task<List<Transaction>> GetTransactionsAsync(int userId) =>
            await databaseService.GetTransactionsAsync(userId);
        public async Task<Transaction> AddTransactionAsync(Transaction transaction) =>
            await databaseService.AddTransactionAsync(transaction);
        public async Task DeleteTransactionAsync(int transactionId) =>
            await databaseService.DeleteTransactionAsync(transactionId);
    }
}