using MyBudgetApp.Models;
using System.Globalization;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public class SavingViewModel(Saving saving, string goalName)
    {
        private static readonly CultureInfo Culture = new("pl-PL");

        public decimal Amount => saving.Amount;

        public string Date => saving.Date.ToString("dd.MM.yyyy", Culture);
        public string Time => saving.Date.ToString("HH:mm", Culture);

        public string Goal { get; } = goalName;
    }
}
