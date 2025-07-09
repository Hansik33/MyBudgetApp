using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using MyBudgetApp.Models;
using System;

namespace MyBudgetApp.ViewModels
{
    public class SavingGoalViewModel
    {
        private readonly SavingGoal _model;

        public SavingGoalViewModel(SavingGoal model)
        {
            _model = model;
        }

        public string Name => _model.Name;
        public decimal TargetAmount => _model.TargetAmount;
        public decimal SavedAmount => _model.SavedAmount;
        public DateTime Deadline => _model.Deadline;

        public double TargetAmountDouble => (double)_model.TargetAmount;
        public double SavedAmountDouble => (double)_model.SavedAmount;

        public string ProgressText =>
            _model.TargetAmount == 0
                ? "Progres: 0%"
                : $"Progres: {Math.Round((_model.SavedAmount / _model.TargetAmount) * 100)}%";

        public Brush ProgressBrush
        {
            get
            {
                if (_model.TargetAmount == 0)
                    return new SolidColorBrush(Colors.Gray);

                double progress = (double)_model.SavedAmount / (double)_model.TargetAmount;

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