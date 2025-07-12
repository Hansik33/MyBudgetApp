using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Auth
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public RegisterViewModel(IUserService userService,
                                 IDialogService dialogService,
                                 INavigationService navigationService)
        {
            _userService = userService;
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

        private async Task Register()
        {
            var errors = AuthenticationValidator
                .Validate(Username, Password, ConfirmPassword)
                .ToList();

            if (errors.Count != 0)
            {
                var firstError = errors.First();
                var message = firstError switch
                {
                    "UsernameEmpty" => AppStrings.Dialogs.Auth.UserEmpty,
                    "PasswordEmpty" => AppStrings.Dialogs.Auth.PasswordEmpty,
                    "PasswordMismatch" => AppStrings.Dialogs.Auth.PasswordMismatch,
                    _ => throw new System.NotImplementedException()
                };

                await _dialogService.ShowMessageAsync(message, DialogType.Warning);
                return;
            }

            var success = _userService.InsertUser(Username, Password);

            if (!success)
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.UserExists, DialogType.Warning);
                return;
            }

            await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.RegisterSuccess, DialogType.Success);
            _navigationService.GoToLogin();
        }
    }
}