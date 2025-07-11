using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IUserContext _userContext;
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<BudgetViewModel> Budgets { get; } = [];
        public ObservableCollection<TransactionViewModel> Transactions { get; } = [];
        public ObservableCollection<SavingViewModel> Savings { get; } = [];
        public ObservableCollection<SavingGoalViewModel> SavingGoals { get; } = [];

        public ICommand LogoutCommand { get; }

        public DashboardViewModel(IUserContext userContext,
                                  IDatabaseService databaseService,
                                  IDialogService dialogService,
                                  INavigationService navigationService)
        {
            _userContext = userContext;
            _databaseService = databaseService;
            _dialogService = dialogService;
            _navigationService = navigationService;

            LogoutCommand = new RelayCommand(async () => await Logout());
            _ = LoadDataAsync();
        }

        public int UserId => _userContext.UserId;
        public string Username => _userContext.Username;

        public decimal SavingAmountTotal => Savings.Sum(saving => saving.Amount);
        public decimal BalanceNumber =>
        Transactions.Where(transaction =>
        transaction.TypeEnum == TransactionType.Income).Sum(transaction => transaction.Amount)
        - Transactions.Where(transaction =>
        transaction.TypeEnum == TransactionType.Expense).Sum(transaction => transaction.Amount)
        - SavingAmountTotal;

        public string Balance => $"{BalanceNumber:0.00} zł";

        private async Task LoadDataAsync()
        {
            var budgets = await _databaseService.GetBudgetsAsync(UserId);
            var transactions = await _databaseService.GetTransactionsAsync(UserId);
            var savings = await _databaseService.GetSavingsAsync(UserId);
            var savingGoals = await _databaseService.GetSavingGoalsAsync(UserId);

            Budgets.Clear();
            foreach (var budget in budgets)
                Budgets.Add(new BudgetViewModel(budget, transactions));

            Transactions.Clear();
            foreach (var transaction in transactions)
                Transactions.Add(new TransactionViewModel(transaction));

            Savings.Clear();
            foreach (var saving in savings)
                Savings.Add(new SavingViewModel(saving, savingGoals));

            SavingGoals.Clear();
            foreach (var savingGoal in savingGoals)
                SavingGoals.Add(new SavingGoalViewModel(savingGoal));

            OnPropertyChanged(nameof(SavingAmountTotal));
            OnPropertyChanged(nameof(BalanceNumber));
            OnPropertyChanged(nameof(Balance));
        }

        private async Task Logout()
        {
            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserLoggedOut, DialogType.Info);
            _userContext.Clear();
            _navigationService.GoToLogin();
        }
    }
}