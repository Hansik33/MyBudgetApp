using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBudgetApp.Data;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DatabaseService(IPasswordHashService passwordHashService, IConfiguration configuration) : IDatabaseService
    {
        private readonly string _connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' is missing.");


        private DbContextOptions<AppDbContext> CreateOptions() =>
            new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
                .Options;

        public bool TryConnect()
        {
            using var appDbContext = new AppDbContext(CreateOptions());
            bool connected = appDbContext.Database.CanConnect();
            Debug.WriteLine($"Database connection: {connected}");
            return connected;
        }

        public bool AddUser(string username, string plainPassword)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            if (appDbContext.Users.Any(user => user.Username == username))
                return false;

            var hashed = passwordHashService.Hash(plainPassword);

            appDbContext.Users.Add(new User
            {
                Username = username,
                Password_hash = hashed
            });

            appDbContext.SaveChanges();
            return true;
        }

        public User? GetUserByCredentials(string username, string plainPassword)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            var user = appDbContext.Users.FirstOrDefault(user => user.Username == username);
            if (user == null)
                return null;

            return passwordHashService.Verify(plainPassword, user.Password_hash) ? user : null;
        }

        public async Task<List<Budget>> GetBudgetsAsync(int userId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            return await appDbContext.Budgets
                .Include(budget => budget.Category)
                .Where(budget => budget.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesAsync(int userId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            return await appDbContext.Categories
                .Where(category => category.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int userId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            return await appDbContext.Transactions
                .Include(transaction => transaction.Category)
                .Where(transaction => transaction.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Saving>> GetSavingsAsync(int userId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            return await appDbContext.Savings
                .Where(saving => saving.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<SavingGoal>> GetSavingGoalsAsync(int userId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            return await appDbContext.SavingGoals
                .Where(savingGoal => savingGoal.UserId == userId)
                .ToListAsync();
        }

        public async Task<Budget> AddBudgetAsync(Budget budget)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            appDbContext.Budgets.Add(budget);

            await appDbContext.SaveChangesAsync();

            return budget;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            appDbContext.Categories.Add(category);
            await appDbContext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteBudgetAsync(int budgetId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            var budget = await appDbContext.Budgets.FirstOrDefaultAsync(budget => budget.Id == budgetId);
            if (budget is null) return;

            appDbContext.Budgets.Remove(budget);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            var category = await appDbContext.Categories.FirstOrDefaultAsync(ccategory => ccategory.Id == categoryId);
            if (category is null) return;

            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            var transaction = await appDbContext.Transactions.FirstOrDefaultAsync(transaction =>
            transaction.Id == transactionId);
            if (transaction is null) return;

            appDbContext.Transactions.Remove(transaction);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSavingAsync(int savingId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            var saving = await appDbContext.Savings.FirstOrDefaultAsync(saving => saving.Id == savingId);
            if (saving is null) return;

            appDbContext.Savings.Remove(saving);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSavingGoalAsync(int savingGoalId)
        {
            using var appDbContext = new AppDbContext(CreateOptions());

            var savingGoal = await appDbContext.SavingGoals.FirstOrDefaultAsync(savingGoal =>
            savingGoal.Id == savingGoalId);
            if (savingGoal is null) return;

            appDbContext.SavingGoals.Remove(savingGoal);
            await appDbContext.SaveChangesAsync();
        }
    }
}