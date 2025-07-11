using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using MyBudgetApp.Models;
using System;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class SavingGoalViewModel(SavingGoal savingGoalModel)
    {
        private readonly SavingGoal _savingGoalModel = savingGoalModel;

        public string Name => _savingGoalModel.Name;
        public decimal TargetAmount => _savingGoalModel.TargetAmount;
        public decimal SavedAmount => _savingGoalModel.SavedAmount;
        public DateTime Deadline => _savingGoalModel.Deadline;

        public double TargetAmountDouble => (double)_savingGoalModel.TargetAmount;
        public double SavedAmountDouble => (double)_savingGoalModel.SavedAmount;

        public string ProgressText =>
            _savingGoalModel.TargetAmount == 0
                ? "Progres: 0%"
                : $"Progres: {Math.Round((_savingGoalModel.SavedAmount / _savingGoalModel.TargetAmount) * 100)}%";

        public Brush ProgressBrush
        {
            get
            {
                if (_savingGoalModel.TargetAmount == 0)
                    return new SolidColorBrush(Colors.Gray);

                double progress = (double)_savingGoalModel.SavedAmount / (double)_savingGoalModel.TargetAmount;

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