using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using MyBudgetApp.Helpers;
using MyBudgetApp.Interfaces;
using MyBudgetApp.ViewModels;
using System;

namespace MyBudgetApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private ContentControl? _host;

        public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public void Initialize(ContentControl host) => _host = host;

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            if (_host == null)
                throw new InvalidOperationException("NavigationService not initialized.");

            var viewType = ViewLocator.ResolveViewType(typeof(TViewModel));
            var view = (UserControl)Activator.CreateInstance(viewType)!;
            view.DataContext = _serviceProvider.GetRequiredService<TViewModel>();

            _host.Content = view;
        }

        public void GoToLogin() => NavigateTo<LoginViewModel>();
        public void GoToRegister() => NavigateTo<RegisterViewModel>();
    }
}
