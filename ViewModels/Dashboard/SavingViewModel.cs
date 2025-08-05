using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public partial class SavingViewModel(Saving saving, IEnumerable<SavingGoalViewModel> allGoals) : BaseViewModel
    {
        private static readonly CultureInfo Culture = new("pl-PL");

        private ObservableCollection<SavingGoalViewModel> _allGoals = new(allGoals);

        public Saving Model => saving;

        public int Id => saving.Id;

        public int GoalId => saving.GoalId;

        private string _goal = string.Empty;
        public string Goal
        {
            get => _goal;
            set => SetProperty(ref _goal, value);
        }

        public decimal Amount => saving.Amount;
        public string Date => saving.Date.ToString("dd.MM.yyyy", Culture);

        public void UpdateSavingGoalsReference(ObservableCollection<SavingGoalViewModel> savingGoals)
        {
            _allGoals = savingGoals;
            Goal = _allGoals.FirstOrDefault(goal => goal.Id == saving.GoalId)?.Name ?? string.Empty;
        }
    }
}