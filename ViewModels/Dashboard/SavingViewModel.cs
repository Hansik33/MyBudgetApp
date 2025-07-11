using MyBudgetApp.Models;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class SavingViewModel
    {
        private readonly Saving _saving;
        private static readonly CultureInfo Culture = new("pl-PL");

        public SavingViewModel(Saving saving, string goalName)
        {
            _saving = saving;
            Goal = goalName;
        }

        public decimal Amount => _saving.Amount;

        public string Date => _saving.Date.ToString("dd.MM.yyyy", Culture);
        public string Time => _saving.Date.ToString("HH:mm", Culture);

        public string Goal { get; }
    }
}
