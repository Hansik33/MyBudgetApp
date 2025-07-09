using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Budget> Budgets { get; } = [];
        public ObservableCollection<Transaction> Transactions { get; } = [];
        public ObservableCollection<Saving> Savings { get; } = [];
        public ObservableCollection<SavingGoalViewModel> SavingGoals { get; } = [];


        public ICommand LogoutCommand { get; }

        public DashboardViewModel(IUserContext userContext, IDatabaseService databaseService,
            IDialogService dialogService, INavigationService navigationService)
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

        private async Task LoadDataAsync()
        {
            var budgets = await _databaseService.GetBudgetsAsync(UserId);
            var transactions = await _databaseService.GetTransactionsAsync(UserId);
            var savings = await _databaseService.GetSavingsAsync(UserId);
            var savingGoals = await _databaseService.GetSavingGoalsAsync(UserId);

            Budgets.Clear();
            foreach (var budget in budgets) Budgets.Add(budget);

            Transactions.Clear();
            foreach (var transaction in transactions) Transactions.Add(transaction);

            Savings.Clear();
            foreach (var saving in savings) Savings.Add(saving);

            SavingGoals.Clear();
            foreach (var savingGoal in savingGoals)
                SavingGoals.Add(new SavingGoalViewModel(savingGoal));
        }

        private async Task Logout()
        {
            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserLoggedOut, DialogType.Info);

            _userContext.Clear();
            _navigationService.GoToLogin();
        }
    }
}