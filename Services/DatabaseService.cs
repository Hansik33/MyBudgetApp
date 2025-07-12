using Microsoft.EntityFrameworkCore;
using MyBudgetApp.Data;
using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DatabaseService(IPasswordHashService passwordHashService) : IDatabaseService
    {
        private const string ConnectionString =
            "server=localhost;port=3306;database=mybudgetapp;user=root;password=qwertyz1234!";

        public bool TryConnect()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);
            bool connected = appDbContext.Database.CanConnect();

            Debug.WriteLine($"Database connection: {connected}");
            return connected;
        }

        public bool InsertUser(string username, string plainPassword)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

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
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            var user = appDbContext.Users.FirstOrDefault(user => user.Username == username);
            if (user == null)
                return null;

            return passwordHashService.Verify(plainPassword, user.Password_hash) ? user : null;
        }

        public async Task<List<Budget>> GetBudgetsAsync(int userId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            return await appDbContext.Budgets
                .Where(budget => budget.UserId == userId)
                .Join(appDbContext.Categories,
                      budget => budget.CategoryId,
                      category => category.Id,
                      (budget, category) => new Budget
                      {
                          Id = budget.Id,
                          UserId = budget.UserId,
                          CategoryId = budget.CategoryId,
                          Year = budget.Year,
                          MonthNumber = budget.MonthNumber,
                          LimitAmount = budget.LimitAmount,
                          CategoryName = category.Name
                      }).ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int userId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            var joined = await appDbContext.Transactions
                .Where(transaction => transaction.UserId == userId)
                .Join(appDbContext.Categories,
                      transaction => transaction.CategoryId,
                      category => category.Id,
                      (transaction, category) => new { transaction, category })
                .ToListAsync();

            var result = new List<Transaction>();

            foreach (var item in joined)
            {
                var transaction = item.transaction;
                var category = item.category;

                var success = Enum.TryParse<TransactionType>(transaction.Type.ToString(), true, out var parsedType);

                result.Add(new Transaction
                {
                    Id = transaction.Id,
                    UserId = transaction.UserId,
                    CategoryId = transaction.CategoryId,
                    Amount = transaction.Amount,
                    Type = success ? parsedType : TransactionType.Expense,
                    Description = transaction.Description ?? string.Empty,
                    PaymentMethod = transaction.PaymentMethod ?? string.Empty,
                    Date = transaction.Date,
                    CategoryName = category.Name
                });
            }

            return result;
        }

        public async Task<List<Saving>> GetSavingsAsync(int userId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            return await appDbContext.Savings
                .Where(saving => saving.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<SavingGoal>> GetSavingGoalsAsync(int userId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            return await appDbContext.SavingGoals
                .Where(savingGoal => savingGoal.UserId == userId)
                .ToListAsync();
        }

        public async Task DeleteBudgetAsync(int budgetId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            var budget = await appDbContext.Budgets.FirstOrDefaultAsync(budget => budget.Id == budgetId);
            if (budget is null) return;

            appDbContext.Budgets.Remove(budget);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            var transaction = await appDbContext.Transactions.FirstOrDefaultAsync(transaction => transaction.Id == transactionId);
            if (transaction is null) return;

            appDbContext.Transactions.Remove(transaction);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSavingAsync(int savingId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            var saving = await appDbContext.Savings.FirstOrDefaultAsync(saving => saving.Id == savingId);
            if (saving is null) return;

            appDbContext.Savings.Remove(saving);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSavingGoalAsync(int savingGoalId)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString))
                .Options;

            using var appDbContext = new AppDbContext(options);

            var savingGoal = await appDbContext.SavingGoals.FirstOrDefaultAsync(savingGoal => savingGoal.Id == savingGoalId);
            if (savingGoal is null) return;

            appDbContext.SavingGoals.Remove(savingGoal);
            await appDbContext.SaveChangesAsync();
        }
    }
}