using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBudgetApp.Data;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class DatabaseService(IConfiguration configuration,
                                 IDialogService dialogService,
                                 IPasswordHashService passwordHashService) : IDatabaseService
    {
        private readonly string? _connectionString = configuration.GetConnectionString("Default");

        private bool TryConnect()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return false;
            }

            try
            {
                using var connection = new MySqlConnection(_connectionString);

                connection.Open();
                connection.Close();

                return true;
            }
            catch (MySqlException)
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return false;
            }
            catch
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return false;
            }
        }

        private DbContextOptions<AppDbContext>? CreateOptions()
        {
            if (!TryConnect())
                return null;

            return new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
                .Options;
        }

        public bool AddUser(string username, string plainPassword)
        {
            var options = CreateOptions();
            if (options == null)
                return false;

            try
            {
                using var appDbContext = new AppDbContext(options);

                if (appDbContext.Users.Any(user => user.Username == username))
                    return false;

                var hashed = passwordHashService.Hash(plainPassword);

                appDbContext.Users.Add(new User
                {
                    Username = username,
                    Password = hashed
                });

                appDbContext.SaveChanges();
                return true;
            }
            catch (MySqlException)
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return false;
            }
            catch
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return false;
            }
        }

        public User? GetUserByCredentials(string username, string plainPassword)
        {
            var options = CreateOptions();
            if (options == null)
                return null;

            try
            {
                using var appDbContext = new AppDbContext(options);

                var user = appDbContext.Users.FirstOrDefault(user => user.Username == username);
                if (user == null)
                    return null;

                return passwordHashService.Verify(plainPassword, user.Password) ? user : null;
            }
            catch (MySqlException)
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
            catch
            {
                dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
        }

        public async Task<List<Budget>> GetBudgetsAsync(int userId)
        {
            var options = CreateOptions();
            if (options == null)
                return new List<Budget>();

            try
            {
                using var appDbContext = new AppDbContext(options);

                return await appDbContext.Budgets
                    .Include(budget => budget.Category)
                    .Where(budget => budget.UserId == userId)
                    .ToListAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Budget>();
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Budget>();
            }
        }

        public async Task<List<Category>> GetCategoriesAsync(int userId)
        {
            var options = CreateOptions();
            if (options == null)
                return new List<Category>();

            try
            {
                using var appDbContext = new AppDbContext(options);

                return await appDbContext.Categories
                    .Where(category => category.UserId == userId)
                    .ToListAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Category>();
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Category>();
            }
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int userId)
        {
            var options = CreateOptions();
            if (options == null)
                return new List<Transaction>();

            try
            {
                using var appDbContext = new AppDbContext(options);

                return await appDbContext.Transactions
                    .Include(transaction => transaction.Category)
                    .Where(transaction => transaction.UserId == userId)
                    .ToListAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Transaction>();
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Transaction>();
            }
        }

        public async Task<List<Saving>> GetSavingsAsync(int userId)
        {
            var options = CreateOptions();
            if (options == null)
                return new List<Saving>();

            try
            {
                using var appDbContext = new AppDbContext(options);

                return await appDbContext.Savings
                    .Where(saving => saving.UserId == userId)
                    .ToListAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Saving>();
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<Saving>();
            }
        }

        public async Task<List<SavingGoal>> GetSavingGoalsAsync(int userId)
        {
            var options = CreateOptions();
            if (options == null)
                return new List<SavingGoal>();

            try
            {
                using var appDbContext = new AppDbContext(options);

                return await appDbContext.SavingGoals
                    .Where(savingGoal => savingGoal.UserId == userId)
                    .ToListAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<SavingGoal>();
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return new List<SavingGoal>();
            }
        }

        public async Task<Budget?> AddBudgetAsync(Budget budget)
        {
            var options = CreateOptions();
            if (options == null)
                return null;

            try
            {
                using var appDbContext = new AppDbContext(options);

                appDbContext.Budgets.Add(budget);

                await appDbContext.SaveChangesAsync();

                return budget;
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
        }

        public async Task<Category?> AddCategoryAsync(Category category)
        {
            var options = CreateOptions();
            if (options == null)
                return null;

            try
            {
                using var appDbContext = new AppDbContext(options);

                appDbContext.Categories.Add(category);
                await appDbContext.SaveChangesAsync();

                return category;
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
        }

        public async Task<Transaction?> AddTransactionAsync(Transaction transaction)
        {
            var options = CreateOptions();
            if (options == null)
                return null;

            try
            {
                using var appDbContext = new AppDbContext(options);

                appDbContext.Transactions.Add(transaction);
                await appDbContext.SaveChangesAsync();

                return transaction;
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
        }

        public async Task<Saving?> AddSavingAsync(Saving saving)
        {
            var options = CreateOptions();
            if (options == null)
                return null;

            try
            {
                using var appDbContext = new AppDbContext(options);

                appDbContext.Savings.Add(saving);
                await appDbContext.SaveChangesAsync();

                return saving;
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
        }

        public async Task<SavingGoal?> AddSavingGoalAsync(SavingGoal savingGoal)
        {
            var options = CreateOptions();
            if (options == null)
                return null;

            try
            {
                using var appDbContext = new AppDbContext(options);

                appDbContext.SavingGoals.Add(savingGoal);
                await appDbContext.SaveChangesAsync();

                return savingGoal;
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
                return null;
            }
        }

        public async Task DeleteBudgetAsync(int budgetId)
        {
            var options = CreateOptions();
            if (options == null)
                return;

            try
            {
                using var appDbContext = new AppDbContext(options);

                var budget = await appDbContext.Budgets.FirstOrDefaultAsync(budget => budget.Id == budgetId);
                if (budget is null)
                    return;

                appDbContext.Budgets.Remove(budget);
                await appDbContext.SaveChangesAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var options = CreateOptions();
            if (options == null)
                return;

            try
            {
                using var appDbContext = new AppDbContext(options);

                var category = await appDbContext.Categories.FirstOrDefaultAsync(ccategory => ccategory.Id == categoryId);
                if (category is null)
                    return;

                appDbContext.Categories.Remove(category);
                await appDbContext.SaveChangesAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
        }

        public async Task DeleteTransactionAsync(int transactionId)
        {
            var options = CreateOptions();
            if (options == null)
                return;

            try
            {
                using var appDbContext = new AppDbContext(options);

                var transaction = await appDbContext.Transactions.FirstOrDefaultAsync(transaction =>
                transaction.Id == transactionId);
                if (transaction is null)
                    return;

                appDbContext.Transactions.Remove(transaction);
                await appDbContext.SaveChangesAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
        }

        public async Task DeleteSavingAsync(int savingId)
        {
            var options = CreateOptions();
            if (options == null)
                return;

            try
            {
                using var appDbContext = new AppDbContext(options);

                var saving = await appDbContext.Savings.FirstOrDefaultAsync(saving => saving.Id == savingId);
                if (saving is null)
                    return;

                appDbContext.Savings.Remove(saving);
                await appDbContext.SaveChangesAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
        }

        public async Task DeleteSavingGoalAsync(int savingGoalId)
        {
            var options = CreateOptions();
            if (options == null)
                return;

            try
            {
                using var appDbContext = new AppDbContext(options);

                var savingGoal = await appDbContext.SavingGoals.FirstOrDefaultAsync(savingGoal =>
                savingGoal.Id == savingGoalId);
                if (savingGoal is null)
                    return;

                appDbContext.SavingGoals.Remove(savingGoal);
                await appDbContext.SaveChangesAsync();
            }
            catch (MySqlException)
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
            catch
            {
                await dialogService.ShowMessageAsync(AppStrings.Dialogs.UnableToConnectDatabase, Enums.DialogType.Error);
            }
        }
    }
}