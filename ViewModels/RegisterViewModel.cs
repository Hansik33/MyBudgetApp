using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Helpers;
using MyBudgetApp.Services;
using MyBudgetApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyBudgetApp.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public ICommand GoToLoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private readonly DatabaseService _db;

        public RegisterViewModel()
        {
            _db = new DatabaseService();

            GoToLoginCommand = new RelayCommand(() => NavigationService.GoToLogin());
            RegisterCommand = new RelayCommand(Register);
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

        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await ShowDialog("Uzupełnij wszystkie pola.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await ShowDialog("Hasła się nie zgadzają.");
                return;
            }

            var success = _db.InsertUser(Username, Password);

            if (!success)
            {
                await ShowDialog("Użytkownik już istnieje.");
                return;
            }

            await ShowDialog("Rejestracja zakończona sukcesem.");
            NavigationService.GoToLogin();
        }

        private async Task ShowDialog(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Informacja",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = MainWindow.Instance.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

