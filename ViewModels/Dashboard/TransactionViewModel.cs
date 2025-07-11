using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using System;

public class TransactionViewModel
{
    private readonly Transaction _transaction;

    public TransactionViewModel(Transaction transaction)
    {
        _transaction = transaction;
    }

    public TransactionType TypeEnum => _transaction.Type;

    public string Type => TypeEnum switch
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
    public string Month => new System.Globalization.CultureInfo("pl-PL").TextInfo.ToTitleCase(
        _transaction.Date.ToString("MMMM", new System.Globalization.CultureInfo("pl-PL")));
}
