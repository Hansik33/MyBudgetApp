using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Helpers;
using MyBudgetApp.Views;

namespace MyBudgetApp.Services
{
    public static class NavigationService
    {
        private static ContentControl? _mainContent;

        public static void Initialize(ContentControl contentControl)
        {
            _mainContent = contentControl;
        }

        public static void NavigateTo(UserControl view)
        {
            if (_mainContent is not null)
                _mainContent.Content = view;
        }

        public static void GoToLogin()
        {
            var viewModel = ViewModelFactory.CreateLoginViewModel();
            var view = new LoginView { DataContext = viewModel };
            NavigateTo(view);
        }

        public static void GoToRegister()
        {
            var viewModel = ViewModelFactory.CreateRegisterViewModel();
            var view = new RegisterView { DataContext = viewModel };
            NavigateTo(view);
        }
    }
}

