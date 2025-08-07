using MyBudgetApp.Enums;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class TransactionService(IDialogService dialogService, IDatabaseService databaseService) : ITransactionService
    {
        private static Transaction CreateTransaction(int userId,
                                                     TransactionType type,
                                                     int categoryId,
                                                     decimal amount,
                                                     PaymentMethod method,
                                                     string description,
                                                     DateTime date)
        {
            return new Transaction
            {
                UserId = userId,
                Type = type,
                CategoryId = categoryId,
                Amount = amount,
                Method = method,
                Description = description,
                Date = date
            };
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int userId) =>
            await databaseService.GetTransactionsAsync(userId);

        public async Task<Transaction?> AddTransactionAsync(int userId, IEnumerable<CategoryViewModel> categories)
        {
            var viewModel = await dialogService.ShowAddTransactionDialogAsync(categories);

            if (viewModel != null)
            {
                var transaction = CreateTransaction(userId,
                                                    viewModel.SelectedTypeAsEnum,
                                                    viewModel.SelectedCategoryId,
                                                    viewModel.AmountAsDecimal,
                                                    viewModel.SelectedMethodAsEnum,
                                                    viewModel.Description == string.Empty ? "Brak" : viewModel.Description,
                                                    viewModel.SelectedDateAsDateTime);
                return await databaseService.AddTransactionAsync(transaction);
            }
            return null;
        }

        public async Task DeleteTransactionAsync(int transactionId) =>
            await databaseService.DeleteTransactionAsync(transactionId);
    }
}