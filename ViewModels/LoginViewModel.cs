using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using System.ComponentModel;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToRegisterCommand = new RelayCommand(() => _navigationService.GoToRegister());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}