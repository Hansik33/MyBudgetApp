using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IBudgetService _budgetService;
        private readonly ITransactionService _transactionService;
        private readonly ISavingService _savingService;
        private readonly ISavingGoalService _savingGoalService;

        private readonly IUserContext _userContext;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<BudgetViewModel> Budgets { get; } = [];
        public ObservableCollection<TransactionViewModel> Transactions { get; } = [];
        public ObservableCollection<SavingViewModel> Savings { get; } = [];
        public ObservableCollection<SavingGoalViewModel> SavingGoals { get; } = [];

        public ICommand LogoutCommand { get; }

        public ICommand DeleteBudgetCommand { get; }
        public ICommand DeleteTransactionCommand { get; }
        public ICommand DeleteSavingCommand { get; }
        public ICommand DeleteSavingGoalCommand { get; }

        public DashboardViewModel(IBudgetService budgetService,
                                  ITransactionService transactionService,
                                  ISavingService savingService,
                                  ISavingGoalService savingGoalService,
                                  IUserContext userContext,
                                  IDialogService dialogService,
                                  INavigationService navigationService)
        {
            _budgetService = budgetService;
            _transactionService = transactionService;
            _savingService = savingService;
            _savingGoalService = savingGoalService;

            _userContext = userContext;
            _dialogService = dialogService;
            _navigationService = navigationService;

            LogoutCommand = new RelayCommand(async () => await Logout());

            DeleteBudgetCommand = new RelayCommand<BudgetViewModel>(async budget => await DeleteBudget(budget));
            DeleteTransactionCommand = new RelayCommand<TransactionViewModel>(async transaction =>
            await DeleteTransaction(transaction));
            DeleteSavingCommand = new RelayCommand<SavingViewModel>(async saving => await DeleteSaving(saving));
            DeleteSavingGoalCommand = new RelayCommand<SavingGoalViewModel>(async savingGoal =>
            await DeleteSavingGoal(savingGoal));

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

        private async Task DeleteBudget(BudgetViewModel budget)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Budget.ConfirmDelete);

            if (!confirmed)
                return;

            await _budgetService.DeleteBudgetAsync(budget.Id);
            Budgets.Remove(budget);

            await _dialogService.ShowMessageAsync(
                AppStrings.Dialogs.Budget.DeletedSuccess,
                DialogType.Success);

            await LoadDataAsync();
        }

        private async Task DeleteTransaction(TransactionViewModel transaction)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Transaction.ConfirmDelete);

            if (!confirmed)
                return;

            if (TransactionValidator.IsDeletionAllowed(transaction, BalanceNumber))
            {
                await _transactionService.DeleteTransactionAsync(transaction.Id);
                Transactions.Remove(transaction);

                await _dialogService.ShowMessageAsync(
                    AppStrings.Dialogs.Transaction.DeletedSuccess,
                    DialogType.Success);

                await LoadDataAsync();
            }
            else
            {
                await _dialogService.ShowMessageAsync(
                    AppStrings.Dialogs.Transaction.DeletionNotAllowed,
                    DialogType.Error);
            }
        }

        private async Task DeleteSaving(SavingViewModel saving)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Saving.ConfirmDelete);

            if (!confirmed)
                return;

            await _savingService.DeleteSavingAsync(saving.Id);
            Savings.Remove(saving);

            await _dialogService.ShowMessageAsync(
                AppStrings.Dialogs.Saving.DeletedSuccess,
                DialogType.Success);

            await LoadDataAsync();
        }

        private async Task DeleteSavingGoal(SavingGoalViewModel savingGoal)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.SavingGoal.ConfirmDelete);

            if (!confirmed)
                return;

            await _savingGoalService.DeleteSavingGoalAsync(savingGoal.Id);
            SavingGoals.Remove(savingGoal);

            await _dialogService.ShowMessageAsync(
                AppStrings.Dialogs.SavingGoal.DeletedSuccess,
                DialogType.Success);

            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var budgets = await _budgetService.GetBudgetsAsync(UserId);
            var transactions = await _transactionService.GetTransactionsAsync(UserId);
            var savings = await _savingService.GetSavingsAsync(UserId);
            var savingGoals = await _savingGoalService.GetSavingGoalsAsync(UserId);

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
                SavingGoals.Add(new SavingGoalViewModel(savingGoal, savings));

            OnPropertyChanged(nameof(SavingAmountTotal));
            OnPropertyChanged(nameof(BalanceNumber));
            OnPropertyChanged(nameof(Balance));
        }

        private async Task Logout()
        {
            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.UserLoggedOut, DialogType.Info);
            _userContext.Clear();
            _navigationService.GoToLogin();
        }
    }
}