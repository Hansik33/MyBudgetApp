using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class SavingViewModel
    {
        private static readonly CultureInfo Culture = new("pl-PL");

        private readonly Saving _saving;

        public SavingViewModel(Saving saving, IEnumerable<SavingGoal> allGoals)
        {
            _saving = saving;

            var goal = allGoals.FirstOrDefault(savingGoal => savingGoal.Id == saving.GoalId);
            Goal = goal?.Name ?? "Nieznany cel";
        }

        public decimal Amount => _saving.Amount;

        public string Date => _saving.Date.ToString("dd.MM.yyyy", Culture);
        public string Time => _saving.Date.ToString("HH:mm", Culture);

        public string Goal { get; }
    }
}