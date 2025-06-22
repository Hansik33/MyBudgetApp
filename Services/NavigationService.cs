using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels;
using MyBudgetApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public static class NavigationService
    {
        private static ContentControl? _mainContent;

        public static void Initialize(ContentControl contentControl) =>
            _mainContent = contentControl;

        public static void NavigateTo(UserControl view)
        {
            if (_mainContent is not null)
                _mainContent.Content = view;
        }

        public static void GoToLogin() =>
            NavigateTo(new LoginView { DataContext = new LoginViewModel() });

        public static void GoToRegister() =>
            NavigateTo(new RegisterView { DataContext = new RegisterViewModel() });
    }
}

