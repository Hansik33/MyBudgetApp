using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.ViewModels;
using MyBudgetApp.Views;
using System;

namespace MyBudgetApp.Services
{
    public class NavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private ContentControl? _mainContent;

        public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public void Initialize(ContentControl contentControl) => _mainContent = contentControl;

        public void NavigateTo(UserControl view)
        {
            if (_mainContent is not null)
                _mainContent.Content = view;
        }

        public void GoToLogin()
        {
            var view = new LoginView
            {
                DataContext = _serviceProvider.GetRequiredService<LoginViewModel>()
            };
            NavigateTo(view);
        }

        public void GoToRegister()
        {
            var view = new RegisterView
            {
                DataContext = _serviceProvider.GetRequiredService<RegisterViewModel>()
            };
            NavigateTo(view);
        }
    }
}

