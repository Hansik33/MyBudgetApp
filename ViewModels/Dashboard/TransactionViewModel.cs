using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using System;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class TransactionViewModel(Transaction transaction)
    {
        public int Id => transaction.Id;

        public TransactionType TypeEnum => transaction.Type;

        public string Type => TypeEnum switch
        {
            TransactionType.Expense => "Wydatek",
            TransactionType.Income => "Przychód",
            _ => throw new NotImplementedException()
        };

        public string Category => transaction.Category?.Name ?? string.Empty;
        public int CategoryId => transaction.CategoryId;
        public decimal Amount => transaction.Amount;
        public string PaymentMethod => transaction.PaymentMethod;
        public string Description => transaction.Description;
        public DateTime Date => transaction.Date;
        public int Year => transaction.Date.Year;

        public string Month => new CultureInfo("pl-PL")
            .TextInfo.ToTitleCase(transaction.Date.ToString("MMMM", new CultureInfo("pl-PL")));
    }
}