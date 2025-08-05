using MyBudgetApp.Enums;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Resources;
using MyBudgetApp.Validators.Auth;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Auth
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IUserContext _userContext;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public ICommand GoToRegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(IUserContext userContext,
                              IUserService userService,
                              IDialogService dialogService,
                              INavigationService navigationService)
        {
            _userContext = userContext;
            _userService = userService;
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

            if (errors.Count != 0)
            {
                var firstError = errors.First();
                var message = firstError switch
                {
                    "UsernameEmpty" => AppStrings.Dialogs.Auth.UserEmpty,
                    "PasswordEmpty" => AppStrings.Dialogs.Auth.PasswordEmpty,
                    _ => throw new System.NotImplementedException()
                };

                await _dialogService.ShowMessageAsync(message, DialogType.Warning);
                return;
            }

            var user = _userService.GetUserByCredentials(Username, Password);

            if (user == null)
            {
                await _dialogService.ShowMessageAsync(AppStrings.Dialogs.Auth.UserNotFound, DialogType.Error);
                return;
            }

            await _dialogService.ShowMessageAsync(string.Format(AppStrings.Dialogs.Auth.LoginSuccess, user.Username),
                DialogType.Info);

            _userContext.UserId = user.Id;
            _userContext.Username = user.Username;

            _navigationService.GoToDashboard();
        }
    }
}
