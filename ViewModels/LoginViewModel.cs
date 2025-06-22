using MyBudgetApp.Helpers;
using MyBudgetApp.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            GoToRegisterCommand = new RelayCommand(() => NavigationService.GoToRegister());
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
