using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Interfaces;
using System;

namespace MyBudgetApp.Services
{
    public class NavigationService : INavigationService
    {
        private ContentControl? _mainContent;

        public void Initialize(ContentControl contentControl) => _mainContent = contentControl;

        public void NavigateTo<TView, TViewModel>()
            where TView : UserControl, new()
            where TViewModel : class
        {
            if (_mainContent == null)
                throw new InvalidOperationException("NavigationService has not been initialized.");

            var view = new TView();
            var viewModel = App.ServiceProvider!.GetRequiredService<TViewModel>();
            view.DataContext = viewModel;
            _mainContent.Content = view;
        }

        public void GoToLogin() => NavigateTo<Views.LoginView, ViewModels.LoginViewModel>();

        public void GoToRegister() => NavigateTo<Views.RegisterView, ViewModels.RegisterViewModel>();
    }
}
