using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using MyBudgetApp.Models;
using System;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class BudgetViewModel(Budget budget, decimal usedAmount)
    {
        private readonly Budget _budget = budget;
        private static readonly CultureInfo Culture = new("pl-PL");

        public int Id => _budget.Id;
        public int UserId => _budget.UserId;
        public int CategoryId => _budget.CategoryId;
        public string Category => _budget.CategoryName;
        public int Year => _budget.Year;
        public int MonthNumber => _budget.MonthNumber;
        public string Month => Culture.TextInfo.ToTitleCase(
            new DateTime(Year, MonthNumber, 1).ToString("MMMM", Culture));

        public decimal LimitAmount => _budget.LimitAmount;
        public decimal UsedAmount { get; } = usedAmount;
        public double UsedAmountDouble => (double)UsedAmount;
        public double LimitAmountDouble => (double)LimitAmount;

        public double UsagePercentNumber => LimitAmount == 0 ? 0 : Math.Min((double)(UsedAmount / LimitAmount) * 100.0, 999.0);
        public string UsagePercent => $"{UsagePercentNumber:0.00}%";
        public string UsageLimit => $"{UsedAmount:0.00} / {LimitAmount:0.00} zł";

        public SolidColorBrush UsageBrush
        {
            get
            {
                if (LimitAmount == 0)
                    return new SolidColorBrush(Colors.Gray);

                double usage = UsagePercentNumber / 100.0;

                return usage switch
                {
                    <= 0.5 => new SolidColorBrush(Colors.Green),
                    <= 1.0 => new SolidColorBrush(Colors.Orange),
                    _ => new SolidColorBrush(Colors.Red)
                };
            }
        }

    }
}