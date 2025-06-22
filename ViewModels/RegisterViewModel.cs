using MyBudgetApp.Helpers;
using MyBudgetApp.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private readonly DatabaseService _db;
        private readonly DialogService _dialogService;
        private readonly NavigationService _navigationService;

        public RegisterViewModel(DatabaseService db, DialogService dialogService, NavigationService navigationService)
        {
            _db = db;
            _dialogService = dialogService;
            _navigationService = navigationService;

            GoToLoginCommand = new RelayCommand(() => _navigationService.GoToLogin());
            RegisterCommand = new RelayCommand(async () => await Register());
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

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    OnPropertyChanged(nameof(ConfirmPassword));
                }
            }
        }

        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await _dialogService.ShowMessageAsync("Uzupełnij wszystkie pola.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await _dialogService.ShowMessageAsync("Hasła się nie zgadzają.");
                return;
            }

            var success = _db.InsertUser(Username, Password);

            if (!success)
            {
                await _dialogService.ShowMessageAsync("Użytkownik już istnieje.");
                return;
            }

            await _dialogService.ShowMessageAsync("Rejestracja zakończona sukcesem.");
            _navigationService.GoToLogin();
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}