using MyBudgetApp.Utils;
using System;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddSavingGoalDialogViewModel : BaseViewModel
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, StringFormatter.Format(value));
        }

        private string _targetAmount = string.Empty;
        public string TargetAmount
        {
            get => _targetAmount;
            set => SetProperty(ref _targetAmount, value);
        }
        public decimal TargetAmountAsDecimal
        {
            get
            {
                if (decimal.TryParse(TargetAmount, out var amount))
                {
                    return amount;
                }
                return 0m;
            }
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