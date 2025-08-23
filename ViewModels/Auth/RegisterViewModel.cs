using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Auth;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Auth
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IRegistrationService _registrationService;
        private readonly INavigationService _navigationService;

        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public RegisterViewModel(IRegistrationService registrationService,
                                 INavigationService navigationService)
        {
            _registrationService = registrationService;
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
            if (await _registrationService.RegisterAsync(Username, Password, ConfirmPassword))
                _navigationService.GoToLogin();
        }
    }
}