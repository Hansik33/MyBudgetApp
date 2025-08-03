using MyBudgetApp.Utils;
using System;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddSavingGoalDialogViewModel : BaseViewModel
    {
        private string _savingGoalName = string.Empty;
        public string SavingGoalName
        {
            get => _savingGoalName;
            set => SetProperty(ref _savingGoalName, StringFormatter.Format(value));
        }

        private string _amount = string.Empty;
        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        private DateTimeOffset _selectedDeadline = new(DateTime.Now.AddMonths(1));
        public DateTimeOffset SelectedDeadline
        {
            get => _selectedDeadline;
            set => SetProperty(ref _selectedDeadline, value);
        }
        public DateTime SelectedDeadlineAsDateTime => SelectedDeadline.DateTime;
    }
}