using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Models;
using MyBudgetApp.Resources;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private readonly IUserContext _userContext;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Budget> Budgets { get; } = new();
        public ObservableCollection<Transaction> Transactions { get; } = new();
        public ObservableCollection<SavingGoal> SavingGoals { get; } = new();

        public ICommand LogoutCommand { get; }

        public DashboardViewModel(IUserContext userContext, IDialogService dialogService, INavigationService navigationService)
        {
            _userContext = userContext;
            _navigationService = navigationService;
            _dialogService = dialogService;

            LogoutCommand = new RelayCommand(async () => await Logout());
        }

        public int UserId => _userContext.UserId;
        public string Username => _userContext.Username;

        private async Task Logout()
        {
            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserLoggedOut, DialogType.Info);

            _userContext.Clear();
            _navigationService.GoToLogin();
        }
    }
}