using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using MyBudgetApp.Models;
using System;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class SavingGoalViewModel(SavingGoal savingGoal)
    {
        private static readonly CultureInfo Culture = new("pl-PL");

        public int Id => savingGoal.Id;
        public string Goal => savingGoal.Name;
        public decimal TargetAmount => savingGoal.TargetAmount;
        public decimal SavedAmount => savingGoal.SavedAmount;
        public DateTime DeadlineDateTime => savingGoal.Deadline;
        public string Deadline => DeadlineDateTime.ToString("dd.MM.yyyy", Culture);

        public double TargetAmountDouble => (double)TargetAmount;
        public double SavedAmountDouble => (double)SavedAmount;

        public double ProgressPercentNumber =>
            TargetAmount == 0 ? 0 : Math.Min((double)(SavedAmount / TargetAmount) * 100.0, 999.0);

        public string ProgressPercent => $"{ProgressPercentNumber:0.00}%";

        public string ProgressAmount => $"{SavedAmount:0.00} / {TargetAmount:0.00} zł";

        public SolidColorBrush ProgressBrush
        {
            get
            {
                if (TargetAmount == 0)
                    return new SolidColorBrush(Colors.Gray);

                double progress = ProgressPercentNumber / 100.0;

                return progress switch
                {
                    >= 1.0 => new SolidColorBrush(Colors.Green),
                    >= 0.5 => new SolidColorBrush(Colors.Orange),
                    _ => new SolidColorBrush(Colors.Red)
                };
            }
        }
    }
}