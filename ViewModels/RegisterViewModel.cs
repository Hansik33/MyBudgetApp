using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.Views;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private readonly IDatabaseService _db;
        private readonly IDialogService _dialog;
        private readonly INavigationService _navigationService;

        public RegisterViewModel(IDatabaseService db, IDialogService dialog, INavigationService navigationService)
        {
            _db = db;
            _dialog = dialog;
            _navigationService = navigationService;

            GoToLoginCommand = new RelayCommand(() => _navigationService.NavigateTo<LoginView, LoginViewModel>());
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
                await _dialog.ShowMessageAsync("Uzupełnij wszystkie pola.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await _dialog.ShowMessageAsync("Hasła się nie zgadzają.");
                return;
            }

            var success = _db.InsertUser(Username, Password);

            if (!success)
            {
                await _dialog.ShowMessageAsync("Użytkownik już istnieje.");
                return;
            }

            await _dialog.ShowMessageAsync("Rejestracja zakończona sukcesem.");
            _navigationService.NavigateTo<LoginView, LoginViewModel>();
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}