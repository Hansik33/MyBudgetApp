using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private readonly IDatabaseService _db;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public RegisterViewModel(IDatabaseService db, IDialogService dialogService, INavigationService navigationService)
        {
            _db = db;
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
    }
}