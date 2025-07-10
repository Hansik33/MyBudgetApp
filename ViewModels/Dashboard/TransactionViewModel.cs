using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using System;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class TransactionViewModel
    {
        private readonly Transaction _transaction;

        public TransactionViewModel(Transaction transaction) => _transaction = transaction;

        public TransactionType Type =>
            Enum.TryParse<TransactionType>(_transaction.Type, out var type)
                ? type
                : TransactionType.Expense;

        public string TypeText => Type switch
        {
            TransactionType.Expense => "Wydatek",
            TransactionType.Income => "Przychód",
            _ => throw new NotImplementedException()
        };

        public decimal Amount => _transaction.Amount;
        public string Description => _transaction.Description;
        public DateTime Date => _transaction.Date;
        public string CategoryName => _transaction.CategoryName;
    }
}
