using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using System;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class TransactionViewModel(Transaction transaction)
    {
        public Transaction Model => transaction;

        public int Id => transaction.Id;

        public TransactionType TransactionType => transaction.Type;
        public string Type => TransactionType switch
        {
            TransactionType.Income => "Przychód",
            TransactionType.Expense => "Wydatek",
            _ => throw new NotImplementedException()
        };

        public int CategoryId => transaction.CategoryId;
        public string Category => transaction.Category?.Name ?? string.Empty;

        public decimal Amount => transaction.Amount;

        public PaymentMethod PaymentMethod => transaction.Method;
        public string Method => PaymentMethod switch
        {
            PaymentMethod.Cash => "Gotówka",
            PaymentMethod.Transfer => "Przelew",
            PaymentMethod.Card => "Karta",
            PaymentMethod.Mobile => "Płatność mobilna",
            PaymentMethod.Other => "Inne",
            _ => throw new NotImplementedException()
        };

        public string Description => transaction.Description;

        public DateTime RawDate => transaction.Date;
        public string Date => RawDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
    }
}