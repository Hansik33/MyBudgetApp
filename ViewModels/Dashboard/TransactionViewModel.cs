using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using System;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class TransactionViewModel(Transaction transaction)
    {
        private readonly Transaction _transaction = transaction;
        private static readonly CultureInfo Culture = new("pl-PL");

        public TransactionType transactionType =>
        Enum.TryParse<TransactionType>(_transaction.Type, true, out var type)
        ? type
        : TransactionType.Expense;

        public string Type => transactionType switch
        {
            TransactionType.Expense => "Wydatek",
            TransactionType.Income => "Przychód",
            _ => throw new NotImplementedException()
        };

        public string Category => _transaction.CategoryName;
        public decimal Amount => _transaction.Amount;
        public string PaymentMethod => _transaction.PaymentMethod;
        public string Description => _transaction.Description;

        public DateTime Date => _transaction.Date;
        public int Year => _transaction.Date.Year;
        public string Month => Culture.TextInfo.ToTitleCase(
            _transaction.Date.ToString("MMMM", Culture));
    }
}