using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToRegisterCommand = new RelayCommand(() => _navigationService.GoToRegister());
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
    }
}