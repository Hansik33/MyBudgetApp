using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Interfaces
{
    public interface IDatabaseService
    {
        bool TryConnect();

        bool AddUser(string username, string passwordHash);
        User? GetUserByCredentials(string username, string plainPassword);

        Task<List<Budget>> GetBudgetsAsync(int userId);
        Task<List<Category>> GetCategoriesAsync(int userId);
        Task<List<Transaction>> GetTransactionsAsync(int userId);
        Task<List<Saving>> GetSavingsAsync(int userId);
        Task<List<SavingGoal>> GetSavingGoalsAsync(int userId);

        Task<Budget> AddBudgetAsync(Budget budget);
        Task<Category> AddCategoryAsync(Category category);
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        Task<Saving> AddSavingAsync(Saving saving);
        Task<SavingGoal> AddSavingGoalAsync(SavingGoal savingGoal);

        Task DeleteBudgetAsync(int budgetId);
        Task DeleteCategoryAsync(int categoryId);
        Task DeleteTransactionAsync(int transactionId);
        Task DeleteSavingAsync(int savingId);
        Task DeleteSavingGoalAsync(int savingGoalId);
    }
}