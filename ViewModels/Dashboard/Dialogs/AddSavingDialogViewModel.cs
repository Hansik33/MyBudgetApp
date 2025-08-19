using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyBudgetApp.ViewModels.Dashboard.Dialogs
{
    public partial class AddSavingDialogViewModel : BaseViewModel
    {
        public ObservableCollection<SavingGoalViewModel> SavingGoals { get; }

        public AddSavingDialogViewModel(IEnumerable<SavingGoalViewModel> savingGoals)
        {
            SavingGoals = new ObservableCollection<SavingGoalViewModel>(savingGoals);

            SelectedSavingGoal = SavingGoals.FirstOrDefault();
        }

        private SavingGoalViewModel? _selectedSavingGoal;
        public SavingGoalViewModel? SelectedSavingGoal
        {
            get => _selectedSavingGoal;
            set => SetProperty(ref _selectedSavingGoal, value, nameof(SelectedSavingGoal));
        }
        public int SelectedSavingGoalId
        {
            get => SelectedSavingGoal?.Id ?? 0;
            set
            {
                if (SelectedSavingGoal != null && SelectedSavingGoal.Id != value)
                    SelectedSavingGoal = SavingGoals.FirstOrDefault(savingGoal => savingGoal.Id == value);

            }
        }

        private string _amount = string.Empty;
        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value, nameof(Amount));
        }
        public decimal AmountAsDecimal
        {
            get
            {
                if (decimal.TryParse(Amount, out var amount))
                    return amount;
                return 0m;
            }
            set => Amount = value.ToString("0.00");
        }
    }
}