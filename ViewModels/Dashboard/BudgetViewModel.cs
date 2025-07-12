using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using MyBudgetApp.Enums;
using MyBudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public partial class BudgetViewModel(Budget budget, IEnumerable<Transaction> transactions) : BaseViewModel
    {
        private static readonly CultureInfo Culture = new("pl-PL");

        public int Id => budget.Id;
        public int UserId => budget.UserId;
        public int CategoryId => budget.CategoryId;
        public string Category => budget.Category?.Name ?? string.Empty;
        public int Year => budget.Year;
        public int MonthNumber => budget.MonthNumber;

        public string Month => Culture.TextInfo.ToTitleCase(
            new DateTime(Year, MonthNumber, 1).ToString("MMMM", Culture));

        public decimal LimitAmount => budget.LimitAmount;

        public decimal UsedAmount { get; } = transactions
            .Where(transaction => transaction.Type == TransactionType.Expense &&
                                  transaction.CategoryId == budget.CategoryId &&
                                  transaction.Date.Year == budget.Year &&
                                  transaction.Date.Month == budget.MonthNumber)
            .Sum(transaction => transaction.Amount);

        public double UsedAmountDouble => (double)UsedAmount;
        public double LimitAmountDouble => (double)LimitAmount;

        public double UsagePercentNumber =>
            LimitAmount == 0 ? 0 : Math.Min((double)(UsedAmount / LimitAmount) * 100.0, 999.0);

        public string UsagePercent => $"{UsagePercentNumber:0.00}%";
        public string UsageLimit => $"{UsedAmount:0.00} / {LimitAmount:0.00} zł";

        public SolidColorBrush UsageBrush =>
            LimitAmount == 0 ? new SolidColorBrush(Colors.Gray)
            : UsagePercentNumber switch
            {
                <= 50 => new SolidColorBrush(Colors.Green),
                <= 100 => new SolidColorBrush(Colors.Orange),
                _ => new SolidColorBrush(Colors.Red)
            };
    }
}