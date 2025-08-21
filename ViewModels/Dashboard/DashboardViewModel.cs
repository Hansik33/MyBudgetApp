using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Dashboard
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IBudgetService _budgetService;
        private readonly ICategoryService _categoryService;
        private readonly ITransactionService _transactionService;
        private readonly ISavingService _savingService;
        private readonly ISavingGoalService _savingGoalService;

        private readonly IUserContext _userContext;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private ObservableCollection<BudgetViewModel> Budgets { get; } = [];
        public IEnumerable<BudgetViewModel> SortedBudgets => Budgets
            .OrderBy(budget => budget.Year).ThenBy(budget => budget.MonthNumber);

        private ObservableCollection<CategoryViewModel> Categories { get; } = [];
        public IEnumerable<CategoryViewModel> SortedCategories => Categories
            .OrderBy(category => category.Name);

        private ObservableCollection<TransactionViewModel> Transactions { get; } = [];
        public IEnumerable<TransactionViewModel> SortedTransactions => Transactions
            .OrderByDescending(transaction => transaction.Date)
            .ThenBy(transaction => transaction.TransactionType);

        public ObservableCollection<SavingViewModel> Savings { get; } = [];

        private ObservableCollection<SavingGoalViewModel> SavingGoals { get; } = [];
        public IEnumerable<SavingGoalViewModel> SortedSavingGoals => SavingGoals
            .OrderBy(savingGoal => savingGoal.DeadlineAsDateTime)
            .ThenBy(savingGoal => savingGoal.Name);

        public ICommand AddBudgetCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand AddSavingCommand { get; }
        public ICommand AddSavingGoalCommand { get; }

        public ICommand DeleteBudgetCommand { get; }
        public ICommand DeleteTransactionCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand DeleteSavingCommand { get; }
        public ICommand DeleteSavingGoalCommand { get; }

        public ICommand LogoutCommand { get; }

        public DashboardViewModel(IBudgetService budgetService,
                                  ITransactionService transactionService,
                                  ICategoryService categoryService,
                                  ISavingService savingService,
                                  ISavingGoalService savingGoalService,
                                  IUserContext userContext,
                                  IDialogService dialogService,
                                  INavigationService navigationService)
        {
            _budgetService = budgetService;
            _categoryService = categoryService;
            _transactionService = transactionService;
            _savingService = savingService;
            _savingGoalService = savingGoalService;

            _userContext = userContext;
            _dialogService = dialogService;
            _navigationService = navigationService;

            AddBudgetCommand = new RelayCommand(async () => await AddBudget());
            AddCategoryCommand = new RelayCommand(async () => await AddCategory());
            AddTransactionCommand = new RelayCommand(async () => await AddTransaction());
            AddSavingCommand = new RelayCommand(async () => await AddSaving());
            AddSavingGoalCommand = new RelayCommand(async () => await AddSavingGoal());

            DeleteBudgetCommand = new RelayCommand<BudgetViewModel>(async budget => await DeleteBudget(budget));
            DeleteCategoryCommand = new RelayCommand<CategoryViewModel>(async category => await DeleteCategory(category));
            DeleteTransactionCommand = new RelayCommand<TransactionViewModel>(async transaction =>
                await DeleteTransaction(transaction));
            DeleteSavingCommand = new RelayCommand<SavingViewModel>(async saving => await DeleteSaving(saving));
            DeleteSavingGoalCommand = new RelayCommand<SavingGoalViewModel>(async savingGoal =>
                await DeleteSavingGoal(savingGoal));

            LogoutCommand = new RelayCommand(async () => await Logout());

            Budgets.CollectionChanged += Budgets_CollectionChanged;
            Categories.CollectionChanged += Categories_CollectionChanged;
            Transactions.CollectionChanged += Transactions_CollectionChanged;
            SavingGoals.CollectionChanged += SavingGoals_CollectionChanged;

            _ = LoadDataAsync();
        }

        public int UserId => _userContext.UserId;
        public string Username => _userContext.Username;

        private decimal SavingAmountTotal => Savings.Sum(saving => saving.Amount);

        private decimal BalanceNumber =>
            Transactions.Where(transaction =>
                transaction.TransactionType == TransactionType.Income).Sum(transaction => transaction.Amount)
            - Transactions.Where(transaction =>
                transaction.TransactionType == TransactionType.Expense).Sum(transaction => transaction.Amount)
            - SavingAmountTotal;
        public string Balance => $"{BalanceNumber:0.00} zł";

        private async Task AddBudget()
        {
            var budget = await _budgetService.AddBudgetAsync(UserId, Budgets, Categories);

            if (budget != null)
            {
                var category = Categories.FirstOrDefault(category => category.Id == budget.CategoryId);

                if (category != null)
                    budget.Category = category.Model;

                Budgets.Add(new BudgetViewModel(budget, Transactions));
            }
        }

        private async Task DeleteBudget(BudgetViewModel budget)
        {
            var success = await _budgetService.DeleteBudgetAsync(budget.Id);

            if (success)
                Budgets.Remove(budget);
        }

        private async Task AddCategory()
        {
            var category = await _categoryService.AddCategoryAsync(UserId, Categories);

            if (category != null)
                Categories.Add(new CategoryViewModel(category));
        }

        private async Task DeleteCategory(CategoryViewModel category)
        {
            var success = await _categoryService.DeleteCategoryAsync(category, Budgets, Transactions);

            if (success)
                Categories.Remove(category);
        }

        private async Task AddTransaction()
        {
            var transaction = await _transactionService.AddTransactionAsync(UserId, Categories, BalanceNumber);

            if (transaction != null)
            {
                var category = Categories.FirstOrDefault(category => category.Id == transaction.CategoryId);

                if (category != null)
                    transaction.Category = category.Model;

                Transactions.Add(new TransactionViewModel(transaction));

                RefreshBudgets();
                UpdateUi();
            }
        }

        private async Task DeleteTransaction(TransactionViewModel transaction)
        {
            var success = await _transactionService.DeleteTransactionAsync(transaction, BalanceNumber);

            if (success)
            {
                Transactions.Remove(transaction);

                RefreshBudgets();
                UpdateUi();
            }
        }

        private async Task AddSaving()
        {
            var saving = await _savingService.AddSavingAsync(UserId, SavingGoals);

            if (saving != null)
                Savings.Add(new SavingViewModel(saving, SavingGoals));
        }

        private async Task DeleteSaving(SavingViewModel saving)
        {
            var success = await _savingService.DeleteSavingAsync(saving.Id);

            if (success)
            {
                Savings.Remove(saving);

                RefreshSavingGoals();
                UpdateUi();
            }
        }

        private async Task AddSavingGoal()
        {
            var savingGoal = await _savingGoalService.AddSavingGoalAsync(UserId, SavingGoals);

            if (savingGoal != null)
                SavingGoals.Add(new SavingGoalViewModel(savingGoal, Savings));
        }

        private async Task DeleteSavingGoal(SavingGoalViewModel savingGoal)
        {
            var success = await _savingGoalService.DeleteSavingGoalAsync(savingGoal.Id);

            if (success)
            {
                var savingsToRemove = Savings.Where(saving => saving.GoalId == savingGoal.Id).ToList();

                foreach (var saving in savingsToRemove)
                    Savings.Remove(saving);

                SavingGoals.Remove(savingGoal);

                UpdateUi();
            }
        }

        private void RefreshBudgets()
        {
            var budgetModels = Budgets.Select(budget => budget.Model).ToList();

            Budgets.Clear();

            foreach (var budget in budgetModels)
                Budgets.Add(new BudgetViewModel(budget, Transactions));
        }

        private void RefreshSavingGoals()
        {
            var savingGoalModels = SavingGoals.Select(savingGoal => savingGoal.Model).ToList();

            SavingGoals.Clear();

            foreach (var savingGoal in savingGoalModels)
                SavingGoals.Add(new SavingGoalViewModel(savingGoal, Savings));
        }

        private async Task<List<Budget>> LoadBudgetsAsync() => await _budgetService.GetBudgetsAsync(UserId);
        private void Budgets_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => OnPropertyChanged(nameof(SortedBudgets));

        private async Task<List<Category>> LoadCategoriesAsync() => await _categoryService.GetCategoriesAsync(UserId);
        private void Categories_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => OnPropertyChanged(nameof(SortedCategories));

        private async Task<List<Transaction>> LoadTransactionsAsync() => await _transactionService.GetTransactionsAsync(UserId);
        private void Transactions_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => OnPropertyChanged(nameof(SortedTransactions));

        private async Task<List<Saving>> LoadSavingsAsync() => await _savingService.GetSavingsAsync(UserId);

        private async Task<List<SavingGoal>> LoadSavingGoalsAsync() => await _savingGoalService.GetSavingGoalsAsync(UserId);
        private void SavingGoals_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => OnPropertyChanged(nameof(SortedSavingGoals));

        private void PopulateBudgets(IEnumerable<Budget> budgets)
        {
            Budgets.Clear();

            foreach (var budget in budgets)
                Budgets.Add(new BudgetViewModel(budget, Transactions));
        }

        private void PopulateCategories(IEnumerable<Category> categories)
        {
            Categories.Clear();

            foreach (var category in categories)
                Categories.Add(new CategoryViewModel(category));
        }

        private void PopulateTransactions(IEnumerable<Transaction> transactions)
        {
            Transactions.Clear();

            foreach (var transaction in transactions)
                Transactions.Add(new TransactionViewModel(transaction));
        }

        private void PopulateSavings(IEnumerable<Saving> savings)
        {
            Savings.Clear();

            foreach (var saving in savings)
                Savings.Add(new SavingViewModel(saving, SavingGoals));
        }

        private void PopulateSavingGoals(IEnumerable<SavingGoal> savingGoals)
        {
            SavingGoals.Clear();

            foreach (var savingGoal in savingGoals)
                SavingGoals.Add(new SavingGoalViewModel(savingGoal, Savings));
        }

        private void UpdateSavingReferences()
        {
            foreach (var saving in Savings)
                saving.UpdateSavingGoalsReference(SavingGoals);

            foreach (var savingGoal in SavingGoals)
                savingGoal.UpdateSavingsReference(Savings);
        }

        private void UpdateUi()
        {
            OnPropertyChanged(nameof(SavingAmountTotal));
            OnPropertyChanged(nameof(BalanceNumber));
            OnPropertyChanged(nameof(Balance));
        }

        private async Task LoadDataAsync()
        {
            var budgets = await LoadBudgetsAsync();
            var categories = await LoadCategoriesAsync();
            var transactions = await LoadTransactionsAsync();
            var savings = await LoadSavingsAsync();
            var savingGoals = await LoadSavingGoalsAsync();

            PopulateCategories(categories);
            PopulateTransactions(transactions);
            PopulateBudgets(budgets);

            PopulateSavings(savings);
            PopulateSavingGoals(savingGoals);
            UpdateSavingReferences();

            UpdateUi();
        }

        private async Task Logout()
        {
            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.UserLoggedOut, DialogType.Info);
            _userContext.Clear();
            _navigationService.GoToLogin();
        }
    }
}