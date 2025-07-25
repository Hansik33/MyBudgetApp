﻿using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<BudgetViewModel> Budgets { get; } = [];
        public ObservableCollection<CategoryViewModel> Categories { get; } = [];
        public ObservableCollection<TransactionViewModel> Transactions { get; } = [];
        public ObservableCollection<SavingViewModel> Savings { get; } = [];
        public ObservableCollection<SavingGoalViewModel> SavingGoals { get; } = [];

        public ICommand LogoutCommand { get; }

        public ICommand AddBudgetCommand { get; }
        public ICommand AddCategoryCommand { get; }

        public ICommand DeleteBudgetCommand { get; }
        public ICommand DeleteTransactionCommand { get; }
        public ICommand DeleteCategoryCommand { get; }
        public ICommand DeleteSavingCommand { get; }
        public ICommand DeleteSavingGoalCommand { get; }

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

            DeleteBudgetCommand = new RelayCommand<BudgetViewModel>(async budget => await DeleteBudget(budget));
            DeleteCategoryCommand = new RelayCommand<CategoryViewModel>(async category => await DeleteCategory(category));
            DeleteTransactionCommand = new RelayCommand<TransactionViewModel>(async transaction =>
                await DeleteTransaction(transaction));
            DeleteSavingCommand = new RelayCommand<SavingViewModel>(async saving => await DeleteSaving(saving));
            DeleteSavingGoalCommand = new RelayCommand<SavingGoalViewModel>(async savingGoal =>
                await DeleteSavingGoal(savingGoal));

            LogoutCommand = new RelayCommand(async () => await Logout());

            _ = LoadDataAsync();
        }

        public int UserId => _userContext.UserId;
        public string Username => _userContext.Username;

        private decimal SavingAmountTotal => Savings.Sum(saving => saving.Amount);
        private decimal BalanceNumber =>
            Transactions.Where(transaction =>
                transaction.TypeEnum == TransactionType.Income).Sum(transaction => transaction.Amount)
            - Transactions.Where(transaction =>
                transaction.TypeEnum == TransactionType.Expense).Sum(transaction => transaction.Amount)
            - SavingAmountTotal;

        public string Balance => $"{BalanceNumber:0.00} zł";

        private async Task AddBudget()
        {
            var budget = await _dialogService.ShowAddBudgetDialogAsync(Categories);

            if (budget != null)
            {
                budget.UserId = UserId;

                var newBudget = await _budgetService.AddBudgetAsync(budget);

                var category = Categories.FirstOrDefault(category => category.Id == newBudget.CategoryId);
                if (category != null)
                    newBudget.Category = category.Model;

                Budgets.Add(new BudgetViewModel(newBudget, Transactions));
            }
        }

        private async Task DeleteBudget(BudgetViewModel budget)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Budget.ConfirmDelete);

            if (!confirmed)
                return;

            await _budgetService.DeleteBudgetAsync(budget.Id);
            Budgets.Remove(budget);

            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Budget.DeletedSuccess, DialogType.Success);
        }

        private async Task AddCategory()
        {
            var category = await _dialogService.ShowAddCategoryDialogAsync(Categories);

            if (category != null)
            {
                category.UserId = UserId;
                var newCategory = await _categoryService.AddCategoryAsync(category);
                Categories.Add(new CategoryViewModel(newCategory));
            }
        }

        private async Task DeleteCategory(CategoryViewModel category)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Category.ConfirmDelete);

            if (!confirmed)
                return;

            if (CategoryValidator.IsDeletionAllowed(category, Budgets, Transactions))
            {
                await _categoryService.DeleteCategoryAsync(category.Id);
                Categories.Remove(category);

                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Category.DeletedSuccess, DialogType.Success);
            }
            else
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Category.DeletionNotAllowed, DialogType.Error);
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

                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Transaction.DeletedSuccess, DialogType.Success);

                RefreshBudgets();
                UpdateUi();
            }
            else
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Transaction.DeletionNotAllowed, DialogType.Error);
        }

        private async Task DeleteSaving(SavingViewModel saving)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.Saving.ConfirmDelete);

            if (!confirmed)
                return;

            await _savingService.DeleteSavingAsync(saving.Id);
            Savings.Remove(saving);

            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Saving.DeletedSuccess, DialogType.Success);

            UpdateUi();
        }

        private async Task DeleteSavingGoal(SavingGoalViewModel savingGoal)
        {
            var confirmed = await _dialogService.ShowConfirmationAsync(AppStrings.Dialogs.SavingGoal.ConfirmDelete);

            if (!confirmed)
                return;

            await _savingGoalService.DeleteSavingGoalAsync(savingGoal.Id);
            SavingGoals.Remove(savingGoal);

            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.SavingGoal.DeletedSuccess, DialogType.Success);
        }

        private void RefreshBudgets()
        {
            var budgetModels = Budgets.Select(budget => budget.Model).ToList();
            Budgets.Clear();
            foreach (var budget in budgetModels)
                Budgets.Add(new BudgetViewModel(budget, Transactions));
        }

        private async Task<List<Budget>> LoadBudgetsAsync() => await _budgetService.GetBudgetsAsync(UserId);

        private async Task<List<Category>> LoadCategoriesAsync() => await _categoryService.GetCategoriesAsync(UserId);

        private async Task<List<Transaction>> LoadTransactionsAsync() => await _transactionService.GetTransactionsAsync(UserId);

        private async Task<List<Saving>> LoadSavingsAsync() => await _savingService.GetSavingsAsync(UserId);

        private async Task<List<SavingGoal>> LoadSavingGoalsAsync() => await _savingGoalService.GetSavingGoalsAsync(UserId);

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

        private void PopulateBudgets(IEnumerable<Budget> budgets)
        {
            Budgets.Clear();
            foreach (var budget in budgets)
                Budgets.Add(new BudgetViewModel(budget, Transactions));
        }

        private void PopulateSavings(IEnumerable<Saving> savings, IEnumerable<SavingGoal> savingGoals)
        {
            Savings.Clear();
            foreach (var saving in savings)
                Savings.Add(new SavingViewModel(saving, savingGoals));
        }

        private void PopulateSavingGoals(IEnumerable<SavingGoal> savingGoals, IEnumerable<Saving> savings)
        {
            SavingGoals.Clear();
            foreach (var savingGoal in savingGoals)
                SavingGoals.Add(new SavingGoalViewModel(savingGoal, savings));
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
            PopulateSavings(savings, savingGoals);
            PopulateSavingGoals(savingGoals, savings);

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