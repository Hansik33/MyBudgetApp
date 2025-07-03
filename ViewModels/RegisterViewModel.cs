using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public RegisterViewModel(IDatabaseService databaseService, IDialogService dialogService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _dialogService = dialogService;
            _navigationService = navigationService;

            GoToLoginCommand = new RelayCommand(() => _navigationService.GoToLogin());
            RegisterCommand = new RelayCommand(async () => await Register());
        }

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool IsUsernameValid() => !string.IsNullOrWhiteSpace(Username);
        private bool IsPasswordValid() => !string.IsNullOrWhiteSpace(Password);
        private bool ArePasswordsMatching() => Password == ConfirmPassword;

        private async Task Register()
        {
            if (!IsUsernameValid())
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserEmpty, DialogType.Warning);
                return;
            }

            if (!IsPasswordValid())
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.PasswordEmpty, DialogType.Warning);
                return;
            }

            if (!ArePasswordsMatching())
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.PasswordMismatch, DialogType.Warning);
                return;
            }

            var success = _databaseService.InsertUser(Username, Password);

            if (!success)
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserExists, DialogType.Warning);
                return;
            }

            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.RegisterSuccess, DialogType.Success);
            _navigationService.GoToLogin();
        }
    }
}