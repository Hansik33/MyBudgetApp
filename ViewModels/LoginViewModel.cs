using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators;
using System.Linq;
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

        public LoginViewModel(IDatabaseService databaseService,
            IDialogService dialogService, INavigationService navigationService)
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

        private async Task Login()
        {
            var errors = AuthenticationValidator
                .Validate(Username, Password)
                .ToList();

            if (errors.Any())
            {
                var firstError = errors.First();
                var message = firstError switch
                {
                    "UsernameEmpty" => AppStrings.Dialogs.UserEmpty,
                    "PasswordEmpty" => AppStrings.Dialogs.PasswordEmpty,
                    _ => throw new System.NotImplementedException()
                };

                await _dialogService.ShowMessageAsync(message, DialogType.Warning);
                return;
            }

            var success = _databaseService.AuthenticateUser(Username, Password);

            if (!success)
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.UserNotFound, DialogType.Error);
                return;
            }

            await _dialogService.ShowMessageAsync(string.Format(AppStrings.Dialogs.LoginSuccess, Username), DialogType.Info);
            _navigationService.GoToDashboard();
        }
    }
}