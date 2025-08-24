using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Auth;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels.Auth
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;

        public ICommand GoToRegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(ILoginService loginService,
                              INavigationService navigationService)
        {
            _loginService = loginService;
            _navigationService = navigationService;

            GoToRegisterCommand = new RelayCommand(() => _navigationService.GoToRegister());
            LoginCommand = new RelayCommand(async () => await Login());
        }

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value.Trim().Replace(" ", ""));
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private async Task Login()
        {
            if (await _loginService.LoginAsync(Username, Password))
                _navigationService.GoToDashboard();
        }
    }
}