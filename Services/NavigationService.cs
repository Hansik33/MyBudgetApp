using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Interfaces;
using MyBudgetApp.ViewModels;
using MyBudgetApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudgetApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ContentControl _mainContent;

        public NavigationService(ContentControl mainContent)
        {
            _mainContent = mainContent;
        }

        public void NavigateTo(UserControl view)
        {
            _mainContent.Content = view;
        }

        public void GoToLogin() =>
            NavigateTo(new LoginView { DataContext = new LoginViewModel(this) });

        public void GoToRegister() =>
            NavigateTo(new RegisterView { DataContext = new RegisterViewModel(this) });
    }
}

