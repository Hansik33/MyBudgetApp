using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ICommand GoToRegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(IDatabaseService databaseService, IDialogService dialogService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _dialogService = dialogService;
            _navigationService = navigationService;

            GoToRegisterCommand = new RelayCommand(() => _navigationService.GoToRegister());
            LoginCommand = new RelayCommand(async () => await Login());
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

        private bool IsUsernameValid() => !string.IsNullOrWhiteSpace(Username);
        private bool IsPasswordValid() => !string.IsNullOrWhiteSpace(Password);

        private async Task Login()
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

            var success = _databaseService.AuthenticateUser(Username, Password);

            if (!success)
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserNotFound, DialogType.Error);
                return;
            }

            await _dialogService.ShowMessageAsync(string.Format(AppStrings.Dialogs.LoginSuccess, Username), DialogType.Info);
            _navigationService.GoToLogin();
        }
    }
}