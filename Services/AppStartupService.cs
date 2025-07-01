using Microsoft.UI.Xaml;
using MyBudgetApp.Interfaces;
using MyBudgetApp.ViewModels;

namespace MyBudgetApp.Services
{
    public class AppStartupService
    {
        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;

        private Window? _window;

        public AppStartupService(INavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }

        public void Start()
        {
            _databaseService.TryConnect();

            _window = new MainWindow();
            _window.Activate();

            if (_window is MainWindow mw)
            {
                _navigationService.Initialize(mw.MainContent);
                _navigationService.NavigateTo<LoginViewModel>();
            }
        }
    }
}
