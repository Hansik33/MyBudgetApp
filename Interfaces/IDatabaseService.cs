using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces
{
    public interface IDatabaseService
    {
        bool TryConnect();

        bool InsertUser(string username, string passwordHash);
        User? GetUserByCredentials(string username, string plainPassword);

        Task<List<Budget>> GetBudgetsAsync(int userId);
        Task<List<Transaction>> GetTransactionsAsync(int userId);
        Task<List<Saving>> GetSavingsAsync(int userId);
        Task<List<SavingGoal>> GetSavingGoalsAsync(int userId);

        Task DeleteBudgetAsync(int budgetId);
        Task DeleteTransactionAsync(int transactionId);
        Task DeleteSavingAsync(int savingId);
        Task DeleteSavingGoalAsync(int savingGoalId);
    }
}