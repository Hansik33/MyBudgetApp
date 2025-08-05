using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using MyBudgetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class SavingGoalViewModel(SavingGoal savingGoal, IEnumerable<SavingViewModel> savings)
    {
        private static readonly CultureInfo Culture = new("pl-PL");

        private ObservableCollection<SavingViewModel> _savings = new(savings);

        public SavingGoal Model => savingGoal;

        public int Id => savingGoal.Id;
        public string Name => savingGoal.Name;

        public decimal TargetAmount => savingGoal.TargetAmount;
        public decimal SavedAmount => _savings
            .Where(saving => saving.GoalId == savingGoal.Id)
            .Sum(saving => saving.Amount);

        public DateTime DeadlineDateTime => savingGoal.Deadline;
        public string Deadline => DeadlineDateTime.ToString("dd.MM.yyyy", Culture);

        public double TargetAmountDouble => (double)TargetAmount;
        public double SavedAmountDouble => (double)SavedAmount;

        public double ProgressPercentNumber =>
            TargetAmount == 0 ? 0 : Math.Min((double)(SavedAmount / TargetAmount) * 100.0, 999.0);
        public string ProgressPercent => $"{ProgressPercentNumber:0}%";
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

        public void UpdateSavingsReference(ObservableCollection<SavingViewModel> savings) => _savings = savings;
    }
}